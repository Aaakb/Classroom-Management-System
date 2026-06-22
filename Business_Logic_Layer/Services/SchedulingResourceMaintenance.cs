using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal static class SchedulingResourceMaintenance
    {
        private const int MinimumLectureRoomCount = 10;
        private const int MinimumLabCount = 10;
        private const int DefaultCapacity = 40;

        public static async Task<SchedulingResourceMaintenanceResult> EnsureOfficialResourcesAsync(AppDbContext context)
        {
            int addedTimeSlots = await EnsureOfficialTimeSlotsAsync(context);
            int addedClassrooms = await EnsureMinimumClassroomsAsync(context);

            return new SchedulingResourceMaintenanceResult(addedTimeSlots, addedClassrooms);
        }

        private static async Task<int> EnsureOfficialTimeSlotsAsync(AppDbContext context)
        {
            var timeSlots = await context.TimeSlots.ToListAsync();
            var usedIds = timeSlots.Select(slot => slot.TimeSlotID).ToHashSet();
            int addedCount = 0;

            foreach (var officialSlot in ScheduleTimingRules.OfficialLectureSlots)
            {
                bool exists = timeSlots.Any(slot =>
                    !slot.IsBreak &&
                    slot.StartTime == officialSlot.Start &&
                    slot.EndTime == officialSlot.End);

                if (exists)
                {
                    continue;
                }

                var timeSlot = new TimeSlot
                {
                    TimeSlotID = NextAvailableId(usedIds),
                    StartTime = officialSlot.Start,
                    EndTime = officialSlot.End,
                    IsBreak = false
                };

                await context.TimeSlots.AddAsync(timeSlot);
                timeSlots.Add(timeSlot);
                addedCount++;
            }

            if (addedCount > 0)
            {
                await ManualKeySaveHelper.SaveWithManualKeyAsync(context, "[TimeSlots]");
            }

            return addedCount;
        }

        private static async Task<int> EnsureMinimumClassroomsAsync(AppDbContext context)
        {
            var classrooms = await context.Classrooms.ToListAsync();
            var usedIds = classrooms.Select(classroom => classroom.ClassroomID).ToHashSet();
            var usedNames = classrooms
                .Select(classroom => classroom.ClassroomNumber)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            int capacity = Math.Max(
                DefaultCapacity,
                await context.Sections.MaxAsync(section => (int?)section.StudentCount) ?? DefaultCapacity);

            int lectureRoomCount = classrooms.Count(IsLectureRoom);
            int labCount = classrooms.Count(IsLab);
            int addedCount = 0;

            while (lectureRoomCount < MinimumLectureRoomCount)
            {
                var classroom = CreateClassroom(
                    usedIds,
                    usedNames,
                    "Lecture Hall",
                    "Lecture",
                    capacity);

                await context.Classrooms.AddAsync(classroom);
                classrooms.Add(classroom);
                lectureRoomCount++;
                addedCount++;
            }

            while (labCount < MinimumLabCount)
            {
                var classroom = CreateClassroom(
                    usedIds,
                    usedNames,
                    "Lab",
                    "Lab",
                    capacity);

                await context.Classrooms.AddAsync(classroom);
                classrooms.Add(classroom);
                labCount++;
                addedCount++;
            }

            if (addedCount > 0)
            {
                await ManualKeySaveHelper.SaveWithManualKeyAsync(context, "[Classrooms]");
            }

            return addedCount;
        }

        private static Classroom CreateClassroom(
            HashSet<int> usedIds,
            HashSet<string> usedNames,
            string namePrefix,
            string roomType,
            int capacity)
        {
            return new Classroom
            {
                ClassroomID = NextAvailableId(usedIds),
                ClassroomNumber = NextClassroomName(usedNames, namePrefix),
                Capacity = capacity,
                RoomType = roomType
            };
        }

        private static int NextAvailableId(HashSet<int> usedIds)
        {
            int nextId = 1;

            while (usedIds.Contains(nextId))
            {
                nextId++;
            }

            usedIds.Add(nextId);
            return nextId;
        }

        private static string NextClassroomName(HashSet<string> usedNames, string prefix)
        {
            int number = 1;
            string roomName;

            do
            {
                roomName = $"{prefix} {number:00}";
                number++;
            }
            while (usedNames.Contains(roomName));

            usedNames.Add(roomName);
            return roomName;
        }

        private static bool IsLab(Classroom classroom)
        {
            return string.Equals(classroom.RoomType, "Lab", StringComparison.OrdinalIgnoreCase) ||
                classroom.ClassroomNumber.Contains("Lab", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLectureRoom(Classroom classroom)
        {
            return !IsLab(classroom);
        }
    }

    internal sealed record SchedulingResourceMaintenanceResult(int AddedTimeSlots, int AddedClassrooms);
}
