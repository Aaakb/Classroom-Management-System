using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class StudyYearService
    {
        public async Task<List<StudyYear>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            var studyYears = await context.StudyYears.AsNoTracking().ToListAsync();

            return studyYears
                .OrderBy(sy => StudyYearRules.GetSortOrder(StudyYearRules.ResolveLevel(sy.YearName, sy.StudyYearID)))
                .ThenBy(sy => sy.YearName)
                .ToList();
        }

        public async Task<StudyYear?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.StudyYears.AsNoTracking().FirstOrDefaultAsync(sy => sy.StudyYearID == id);
        }

        public async Task<StudyYear> AddAsync(StudyYear studyYear)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, studyYear, false);
            await context.StudyYears.AddAsync(studyYear);
            await context.SaveChangesAsync();
            return studyYear;
        }

        public async Task<StudyYear> UpdateAsync(StudyYear studyYear)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, studyYear, true);
            context.StudyYears.Update(studyYear);
            await context.SaveChangesAsync();
            return studyYear;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var studyYear = await context.StudyYears.FindAsync(id)
                ?? throw new KeyNotFoundException("Study year not found.");

            context.StudyYears.Remove(studyYear);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateAsync(AppDbContext context, StudyYear studyYear, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(studyYear.YearName))
            {
                throw new ArgumentException("Study year name is required.");
            }

            var level = StudyYearRules.ResolveLevel(studyYear.YearName, studyYear.StudyYearID);

            if (level == StudyYearLevel.Unknown)
            {
                throw new ArgumentException("Study year must be First, Second, Third, or Fourth year.");
            }

            studyYear.YearName = StudyYearRules.GetDisplayName(level);
            var exists = await context.StudyYears.AnyAsync(sy =>
                sy.YearName == studyYear.YearName && (!isUpdate || sy.StudyYearID != studyYear.StudyYearID));

            if (exists)
            {
                throw new ArgumentException("Study year name already exists.");
            }
        }
    }
}
