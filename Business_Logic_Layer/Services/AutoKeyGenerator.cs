using Microsoft.EntityFrameworkCore;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal static class AutoKeyGenerator
    {
        public static async Task<int> NextAsync(IQueryable<int> keys)
        {
            return await keys.AnyAsync()
                ? await keys.MaxAsync() + 1
                : 1;
        }
    }
}
