using CsvHelper.Configuration.Attributes;

namespace YourNamespace.Models
{
    public class AttendanceDeclaration
    {
        [Name("StudentName")]
        public string StudentName { get; set; } = string.Empty;

        [Name("ExamDate")]
        public DateTime ExamDate { get; set; }

        [Name("ExamName")]
        public string ExamName { get; set; } = string.Empty;

        [Name("StartTime")]
        public string StartTime { get; set; } = string.Empty;

        [Name("EndTime")]
        public string EndTime { get; set; } = string.Empty;

        [Name("StudentEmail")]
        public string StudentEmail { get; set; } = string.Empty;

        [Name("ContactNumber")]
        public string ContactNumber { get; set; } = string.Empty;

        [Name("Location")]
        public string Location { get; set; } = string.Empty;

        [Name("CurrentDate")]
        public DateTime CurrentDate { get; set; }

        [Name("TeacherName")]
        public string TeacherName { get; set; } = string.Empty;

        [Name("TeacherPosition")]
        public string TeacherPosition { get; set; } = string.Empty;
    }
}
