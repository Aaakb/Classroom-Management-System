using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal class SectionService
    {
        public async Task<List<Section>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            return await context.Sections
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.Sections
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SectionID == id);
        }

        public async Task<Section> AddAsync(Section section)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, section, false);
            await context.Sections.AddAsync(section);
            await context.SaveChangesAsync();
            return section;
        }

        public async Task<Section> UpdateAsync(Section section)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, section, true);
            context.Sections.Update(section);
            await context.SaveChangesAsync();
            return section;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var section = await context.Sections.FindAsync(id)
                ?? throw new KeyNotFoundException("Section not found.");

            context.Sections.Remove(section);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateAsync(AppDbContext context, Section section, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(section.SectionName))
            {
                throw new ArgumentException("Section name is required.");
            }

            if (section.StudentCount < 0)
            {
                throw new ArgumentException("Student count cannot be negative.");
            }

            var studyYear = await context.StudyYears
                .AsNoTracking()
                .FirstOrDefaultAsync(sy => sy.StudyYearID == section.StudyYearID);

            if (studyYear is null)
            {
                throw new ArgumentException("Study year does not exist.");
            }

            var level = StudyYearRules.ResolveLevel(studyYear.YearName, studyYear.StudyYearID);

            if (level == StudyYearLevel.Unknown)
            {
                throw new ArgumentException("Study year must be First, Second, Third, or Fourth year.");
            }

            if (StudyYearRules.UsesGeneralSections(level) && section.BranchID.HasValue)
            {
                throw new ArgumentException("First and second year sections must not be linked to a branch.");
            }

            if (StudyYearRules.UsesBranches(level) && !section.BranchID.HasValue)
            {
                throw new ArgumentException("Third and fourth year sections must be linked to a branch.");
            }

            if (section.BranchID.HasValue &&
                !await context.Branches.AnyAsync(b => b.BranchID == section.BranchID.Value))
            {
                throw new ArgumentException("Branch does not exist.");
            }

            section.SectionName = section.SectionName.Trim();

            var allowedSectionNames = StudyYearRules.GetAllowedSectionNames(level);

            if (allowedSectionNames.Count > 0 &&
                !allowedSectionNames.Contains(section.SectionName, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"Allowed section names for this study year are: {StudyYearRules.FormatAllowedSectionNames(level)}.");
            }

            var exists = await context.Sections.AnyAsync(s =>
                s.StudyYearID == section.StudyYearID &&
                s.BranchID == section.BranchID &&
                s.SectionName == section.SectionName &&
                (!isUpdate || s.SectionID != section.SectionID));

            if (exists)
            {
                throw new ArgumentException("Section already exists for the same study year and branch.");
            }
        }
    }
}
