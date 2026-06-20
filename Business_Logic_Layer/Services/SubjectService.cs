using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class SubjectService
    {
        public async Task<List<Subject>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            return await context.Subjects
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.Subjects
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SubjectID == id);
        }

        public async Task<Subject> AddAsync(Subject subject)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, subject, false);
            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, subject, true);
            context.Subjects.Update(subject);
            await context.SaveChangesAsync();
            return subject;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var subject = await context.Subjects.FindAsync(id)
                ?? throw new KeyNotFoundException("Subject not found.");

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateAsync(AppDbContext context, Subject subject, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                throw new ArgumentException("Subject name is required.");
            }

            if (subject.SemesterNumber <= 0)
            {
                throw new ArgumentException("Semester number must be greater than zero.");
            }

            if (subject.TheoreticalHours < 0 || subject.PracticalHours < 0 || subject.CreditUnits < 0)
            {
                throw new ArgumentException("Hours and credit units cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(subject.RequirementType))
            {
                throw new ArgumentException("Requirement type is required.");
            }

            var studyYear = await context.StudyYears
                .AsNoTracking()
                .FirstOrDefaultAsync(sy => sy.StudyYearID == subject.StudyYearID);

            if (studyYear is null)
            {
                throw new ArgumentException("Study year does not exist.");
            }

            var level = StudyYearRules.ResolveLevel(studyYear.YearName, studyYear.StudyYearID);

            if (level == StudyYearLevel.Unknown)
            {
                throw new ArgumentException("Study year must be First, Second, Third, or Fourth year.");
            }

            if (StudyYearRules.UsesGeneralSections(level) && subject.BranchID.HasValue)
            {
                throw new ArgumentException("First and second year subjects must be general and not linked to a branch.");
            }

            if (StudyYearRules.UsesBranches(level) && !subject.BranchID.HasValue)
            {
                throw new ArgumentException("Third and fourth year subjects must be linked to a branch.");
            }

            if (subject.BranchID.HasValue &&
                !await context.Branches.AnyAsync(b => b.BranchID == subject.BranchID.Value))
            {
                throw new ArgumentException("Branch does not exist.");
            }

            subject.SubjectName = subject.SubjectName.Trim();
            subject.RequirementType = subject.RequirementType.Trim();

            var exists = await context.Subjects.AnyAsync(s =>
                s.SubjectName == subject.SubjectName &&
                s.StudyYearID == subject.StudyYearID &&
                s.SemesterNumber == subject.SemesterNumber &&
                (!isUpdate || s.SubjectID != subject.SubjectID));

            if (exists)
            {
                throw new ArgumentException("Subject already exists in the same study year and semester.");
            }
        }
    }
}
