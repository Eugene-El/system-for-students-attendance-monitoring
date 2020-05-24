using SAMS.BusinessLogic.Entities.Enumerations;

namespace SAMS.Database.EF.EntitiesDb
{
    public class NotificationRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NotificationMethod NotificationMethod { get; set; }
        public int StudyProgrammeId { get; set; }
        public StudyProgramme StudyProgramme { get; set; }
        public SudentLanguage Language { get; set; }
        public LearningForm LearningForm { get; set; }
        public int AttendancePeriod { get; set; }
        public int AttendanceProcent { get; set; }
        public string Message { get; set; }


        public BusinessLogic.Entities.NotificationRule MapToEntity()
        {
            return new BusinessLogic.Entities.NotificationRule
            {
                Id = this.Id,
                Name = this.Name,
                NotificationMethod = this.NotificationMethod,
                StudyProgrammeId = this.StudyProgrammeId,
                Language = this.Language,
                LearningForm = this.LearningForm,
                AttendancePeriod = this.AttendancePeriod,
                AttendanceProcent = this.AttendanceProcent,
                Message = this.Message
            };
        }

        public NotificationRule MapFromEntity(BusinessLogic.Entities.NotificationRule notificationRule)
        {
            Id = notificationRule.Id;
            Name = notificationRule.Name;
            NotificationMethod = notificationRule.NotificationMethod;
            StudyProgrammeId = notificationRule.StudyProgrammeId;
            Language = notificationRule.Language;
            LearningForm = notificationRule.LearningForm;
            AttendancePeriod = notificationRule.AttendancePeriod;
            AttendanceProcent = notificationRule.AttendanceProcent;
            Message = notificationRule.Message;
            return this;
        }
    }
}
