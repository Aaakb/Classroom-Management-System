using System.Drawing;
using System.Windows.Forms;

namespace University_Timetable_and_Classroom_Management_System
{
    public partial class SchedulesForm
    {
        private void ConfigureScheduleGrid()
        {
            dgvSchedules.AutoGenerateColumns = false;
            dgvSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedules.BackgroundColor = Color.White;
            dgvSchedules.BorderStyle = BorderStyle.None;
            dgvSchedules.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSchedules.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSchedules.ColumnHeadersHeight = 48;
            dgvSchedules.EnableHeadersVisualStyles = false;
            dgvSchedules.RowTemplate.Height = 46;
            dgvSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridStyle.Apply(dgvSchedules);
            dgvSchedules.DataSource = scheduleBindingSource;
            colScheduleId.DataPropertyName = nameof(ScheduleRow.ScheduleID);
            colScheduleId.Visible = false;
            EnsureSemesterColumn();
            colDayOfWeek.DataPropertyName = nameof(ScheduleRow.DayOfWeek);
            colSubject.DataPropertyName = nameof(ScheduleRow.SubjectName);
            colFacultyMember.DataPropertyName = nameof(ScheduleRow.FacultyMemberName);
            colClassroom.DataPropertyName = nameof(ScheduleRow.ClassroomName);
            colTimeSlot.DataPropertyName = nameof(ScheduleRow.TimeSlotName);
            colTimeSlot.HeaderText = "Time";
            colStudyYear.DataPropertyName = nameof(ScheduleRow.StudyYearName);
            colBranch.DataPropertyName = nameof(ScheduleRow.BranchName);
            colSection.DataPropertyName = nameof(ScheduleRow.SectionName);
            ApplyScheduleGridColumnLayout();
        }

        private void EnsureSemesterColumn()
        {
            if (dgvSchedules.Columns.Contains("colSemester"))
            {
                return;
            }

            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.SemesterNumber),
                FillWeight = 58F,
                HeaderText = "Semester",
                Name = "colSemester",
                ReadOnly = true
            };

            dgvSchedules.Columns.Insert(1, column);

            dgvSchedules.Columns.Insert(4, new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.GroupName),
                FillWeight = 56F,
                HeaderText = "Group",
                Name = "colGroupName",
                ReadOnly = true
            });

            dgvSchedules.Columns.Insert(5, new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ScheduleRow.LectureType),
                FillWeight = 64F,
                HeaderText = "Type",
                Name = "colLectureType",
                ReadOnly = true
            });

            colDayOfWeek.DisplayIndex = 0;
            colTimeSlot.DisplayIndex = 1;
            colSubject.DisplayIndex = 2;
            colFacultyMember.DisplayIndex = 3;
            colClassroom.DisplayIndex = 4;
            dgvSchedules.Columns["colLectureType"].DisplayIndex = 5;
            colSection.DisplayIndex = 6;
            dgvSchedules.Columns["colGroupName"].DisplayIndex = 7;
            colStudyYear.DisplayIndex = 8;
            colBranch.DisplayIndex = 9;
            column.DisplayIndex = 10;
        }

        private void ApplyScheduleGridColumnLayout()
        {
            SetGridColumn(colDayOfWeek, "Day", 78);
            SetGridColumn(colTimeSlot, "Time", 138);
            SetGridColumn(colSubject, "Subject", 230);
            SetGridColumn(colFacultyMember, "Teacher", 170);
            SetGridColumn(colClassroom, "Room", 110);
            SetGridColumn(dgvSchedules.Columns["colLectureType"], "Type", 92);
            SetGridColumn(colSection, "Section", 86);
            SetGridColumn(dgvSchedules.Columns["colGroupName"], "Group", 64);
            SetGridColumn(colStudyYear, "Year", 90);
            SetGridColumn(colBranch, "Branch", 105);
            SetGridColumn(dgvSchedules.Columns["colSemester"], "Semester", 70);

            colStudyYear.Visible = false;
            colBranch.Visible = false;
            dgvSchedules.Columns["colSemester"].Visible = false;

            dgvSchedules.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            dgvSchedules.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            dgvSchedules.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvSchedules.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvSchedules.RowsDefaultCellStyle.BackColor = Color.White;
            dgvSchedules.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSchedules.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            dgvSchedules.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvSchedules.ColumnHeadersHeight = 48;
            dgvSchedules.RowTemplate.Height = 46;
        }

        private static void SetGridColumn(DataGridViewColumn column, string headerText, float fillWeight)
        {
            column.HeaderText = headerText;
            column.FillWeight = fillWeight;
            column.MinimumWidth = 48;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void StyleScheduleRows(IReadOnlyCollection<ScheduleRow> visibleRows)
        {
            var conflictingScheduleIds = GetConflictingScheduleIds(visibleRows);

            foreach (DataGridViewRow row in dgvSchedules.Rows)
            {
                if (row.DataBoundItem is not ScheduleRow scheduleRow)
                {
                    continue;
                }

                bool isPractical = scheduleRow.LectureType == "Practical";
                bool hasConflict = conflictingScheduleIds.Contains(scheduleRow.ScheduleID);
                row.DefaultCellStyle.BackColor = hasConflict
                    ? Color.FromArgb(254, 242, 242)
                    : isPractical
                        ? Color.FromArgb(240, 253, 244)
                        : Color.White;
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
                row.DefaultCellStyle.SelectionForeColor = Color.White;

                row.Cells["colDayOfWeek"].Style.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                row.Cells["colTimeSlot"].Style.ForeColor = Color.FromArgb(37, 99, 235);

                if (row.Cells["colSubject"] is DataGridViewCell subjectCell)
                {
                    subjectCell.ToolTipText = scheduleRow.SubjectName;
                }
            }
        }
    }
}
