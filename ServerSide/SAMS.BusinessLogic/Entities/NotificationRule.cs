using SAMS.BusinessLogic.Entities.Enumerations;

namespace SAMS.BusinessLogic.Entities
{
    public class NotificationRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NotificationMethod NotificationMethod { get; set; }
        public int StudyProgrammeId { get; set; }
        public SudentLanguage Language { get; set; }
        public LearningForm LearningForm { get; set; }
        public int AttendancePeriod { get; set; }
        public int AttendanceProcent { get; set; }
        public string Message { get; set; }
    }
}
