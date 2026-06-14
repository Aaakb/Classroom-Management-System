using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal class FacultyMemberService
    {
        public async Task<List<FacultyMember>> GetAllAsync()
        {
            await using var context = new AppDbContext();
            return await context.FacultyMembers.AsNoTracking().ToListAsync();
        }

        public async Task<FacultyMember?> GetByIdAsync(int id)
        {
            await using var context = new AppDbContext();
            return await context.FacultyMembers.AsNoTracking().FirstOrDefaultAsync(f => f.FacultyMemberID == id);
        }

        public async Task<FacultyMember> AddAsync(FacultyMember facultyMember)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, facultyMember, false);
            await context.FacultyMembers.AddAsync(facultyMember);
            await context.SaveChangesAsync();
            return facultyMember;
        }

        public async Task<FacultyMember> UpdateAsync(FacultyMember facultyMember)
        {
            await using var context = new AppDbContext();
            await ValidateAsync(context, facultyMember, true);
            context.FacultyMembers.Update(facultyMember);
            await context.SaveChangesAsync();
            return facultyMember;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new AppDbContext();
            var facultyMember = await context.FacultyMembers.FindAsync(id)
                ?? throw new KeyNotFoundException("Faculty member not found.");

            context.FacultyMembers.Remove(facultyMember);
            await context.SaveChangesAsync();
        }

        private static async Task ValidateAsync(AppDbContext context, FacultyMember facultyMember, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(facultyMember.FullName))
            {
                throw new ArgumentException("Faculty member full name is required.");
            }

            facultyMember.FullName = facultyMember.FullName.Trim();
            facultyMember.AcademicTitle = string.IsNullOrWhiteSpace(facultyMember.AcademicTitle)
                ? null
                : facultyMember.AcademicTitle.Trim();

            var exists = await context.FacultyMembers.AnyAsync(f =>
                f.FullName == facultyMember.FullName &&
                (!isUpdate || f.FacultyMemberID != facultyMember.FacultyMemberID));

            if (exists)
            {
                throw new ArgumentException("Faculty member already exists.");
            }
        }
    }
}
