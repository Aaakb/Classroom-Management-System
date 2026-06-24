using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using University_Timetable_and_Classroom_Management_System.Models;

namespace University_Timetable_and_Classroom_Management_System.BusinessLayer
{
    internal static class SchedulingResourceMaintenance
    {
        private const int MinimumLectureRoomCount = 10;
        private const int MinimumLabCount = 10;
        private const int MinimumBackupFacultyCount = 2;
        private const int MinimumFacultyOptionsPerSubject = 2;
        private const int DefaultCapacity = 40;
        private static readonly string[] BackupFacultyNames =
        [
            "Nadia Kareem",
            "Yasir Mahmood"
        ];

        public static async Task<SchedulingResourceMaintenanceResult> EnsureOfficialResourcesAsync(AppDbContext context)
        {
            int addedTimeSlots = await EnsureOfficialTimeSlotsAsync(context);
            await NormalizeClassroomsAsync(context);
            int addedClassrooms = await EnsureMinimumClassroomsAsync(context);
            var facultyCoverage = await EnsureBackupFacultyCoverageAsync(context);

            return new SchedulingResourceMaintenanceResult(
                addedTimeSlots,
                addedClassrooms,
                facultyCoverage.AddedFacultyMembers,
                facultyCoverage.AddedFacultySubjectAssignments);
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

        private static async Task<FacultyCoverageMaintenanceResult> EnsureBackupFacultyCoverageAsync(AppDbContext context)
        {
            var facultyMembers = await context.FacultyMembers
                .OrderBy(facultyMember => facultyMember.FacultyMemberID)
                .ToListAsync();
            var usedIds = facultyMembers
                .Select(facultyMember => facultyMember.FacultyMemberID)
                .ToHashSet();
            var usedNames = facultyMembers
                .Select(facultyMember => facultyMember.FullName)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            var backupFaculty = facultyMembers
                .Where(facultyMember => BackupFacultyNames.Contains(facultyMember.FullName, StringComparer.OrdinalIgnoreCase))
                .ToList();

            int addedFacultyMembers = 0;

            foreach (string facultyName in BackupFacultyNames)
            {
                if (backupFaculty.Count >= MinimumBackupFacultyCount)
                {
                    break;
                }

                if (backupFaculty.Any(facultyMember =>
                    string.Equals(facultyMember.FullName, facultyName, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                var facultyMember = new FacultyMember
                {
                    FacultyMemberID = NextAvailableId(usedIds),
                    FullName = NextFacultyName(usedNames, facultyName),
                    AcademicTitle = "Assistant Lecturer"
                };

                await context.FacultyMembers.AddAsync(facultyMember);
                facultyMembers.Add(facultyMember);
                backupFaculty.Add(facultyMember);
                addedFacultyMembers++;
            }

            if (addedFacultyMembers > 0)
            {
                await ManualKeySaveHelper.SaveWithManualKeyAsync(context, "[FacultyMembers]");
            }

            int addedAssignments = await EnsureMinimumFacultyOptionsForSubjectsAsync(context, backupFaculty);

            return new FacultyCoverageMaintenanceResult(addedFacultyMembers, addedAssignments);
        }

        private static async Task<int> EnsureMinimumFacultyOptionsForSubjectsAsync(
            AppDbContext context,
            IReadOnlyList<FacultyMember> backupFaculty)
        {
            if (backupFaculty.Count == 0)
            {
                return 0;
            }

            var subjectIds = await context.Subjects
                .Select(subject => subject.SubjectID)
                .OrderBy(subjectId => subjectId)
                .ToListAsync();
            var assignments = await context.FacultyMemberSubjects.ToListAsync();
            var assignmentSet = assignments
                .Select(assignment => (assignment.FacultyMemberID, assignment.SubjectID))
                .ToHashSet();
            var subjectFacultyCounts = assignments
                .GroupBy(assignment => assignment.SubjectID)
                .ToDictionary(group => group.Key, group => group.Select(item => item.FacultyMemberID).Distinct().Count());

            int addedAssignments = 0;
            int backupIndex = 0;

            foreach (int subjectId in subjectIds)
            {
                int currentCount = subjectFacultyCounts.GetValueOrDefault(subjectId);
                int attempts = 0;

                while (currentCount < MinimumFacultyOptionsPerSubject &&
                    attempts < backupFaculty.Count)
                {
                    var facultyMember = backupFaculty[backupIndex % backupFaculty.Count];
                    backupIndex++;
                    attempts++;

                    if (!assignmentSet.Add((facultyMember.FacultyMemberID, subjectId)))
                    {
                        continue;
                    }

                    await context.FacultyMemberSubjects.AddAsync(new FacultyMemberSubject
                    {
                        FacultyMemberID = facultyMember.FacultyMemberID,
                        SubjectID = subjectId
                    });

                    currentCount++;
                    subjectFacultyCounts[subjectId] = currentCount;
                    addedAssignments++;
                }
            }

            if (addedAssignments > 0)
            {
                await context.SaveChangesAsync();
            }

            return addedAssignments;
        }

        private static async Task NormalizeClassroomsAsync(AppDbContext context)
        {
            var classrooms = await context.Classrooms
                .OrderBy(classroom => classroom.ClassroomID)
                .ToListAsync();

            if (classrooms.Count == 0)
            {
                return;
            }

            var lectureRooms = classrooms
                .Where(classroom => !IsLab(classroom))
                .OrderBy(classroom => classroom.ClassroomID)
                .ToList();

            var labs = classrooms
                .Where(IsLab)
                .OrderBy(classroom => classroom.ClassroomID)
                .ToList();

            bool needsRename = NeedsClassroomNormalization(lectureRooms, "Lecture Hall", "Lecture") ||
                NeedsClassroomNormalization(labs, "Lab", "Lab");

            if (!needsRename)
            {
                return;
            }

            foreach (var classroom in classrooms)
            {
                classroom.ClassroomNumber = $"__RoomTemp_{classroom.ClassroomID}";
            }

            await context.SaveChangesAsync();

            RenameClassroomGroup(lectureRooms, "Lecture Hall", "Lecture");
            RenameClassroomGroup(labs, "Lab", "Lab");

            await context.SaveChangesAsync();
        }

        private static bool NeedsClassroomNormalization(
            IReadOnlyList<Classroom> classrooms,
            string namePrefix,
            string roomType)
        {
            for (int index = 0; index < classrooms.Count; index++)
            {
                string expectedName = $"{namePrefix} {index + 1:00}";

                if (!string.Equals(classrooms[index].ClassroomNumber, expectedName, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(classrooms[index].RoomType, roomType, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static void RenameClassroomGroup(
            IReadOnlyList<Classroom> classrooms,
            string namePrefix,
            string roomType)
        {
            for (int index = 0; index < classrooms.Count; index++)
            {
                classrooms[index].ClassroomNumber = $"{namePrefix} {index + 1:00}";
                classrooms[index].RoomType = roomType;
            }
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

        private static string NextFacultyName(HashSet<string> usedNames, string preferredName)
        {
            if (usedNames.Add(preferredName))
            {
                return preferredName;
            }

            int suffix = 2;
            string facultyName;

            do
            {
                facultyName = $"{preferredName} {suffix}";
                suffix++;
            }
            while (!usedNames.Add(facultyName));

            return facultyName;
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

    internal sealed record SchedulingResourceMaintenanceResult(
        int AddedTimeSlots,
        int AddedClassrooms,
        int AddedFacultyMembers,
        int AddedFacultySubjectAssignments);

    internal sealed record FacultyCoverageMaintenanceResult(
        int AddedFacultyMembers,
        int AddedFacultySubjectAssignments);
}
