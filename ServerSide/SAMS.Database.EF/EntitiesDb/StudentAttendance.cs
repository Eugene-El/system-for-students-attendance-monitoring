using System;
using System.Collections.Generic;
using System.Text;

namespace SAMS.Database.EF.EntitiesDb
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime Date { get; set; }

        public int NecessaryAttendance { get; set; }
        public int RealAttendance { get; set; }


        public BusinessLogic.Entities.StudentAttendance MapToEntity()
        {
            return new BusinessLogic.Entities.StudentAttendance
            {
                Id = this.Id,
                StudentId = this.StudentId,
                SubjectId = this.SubjectId,
                Date = this.Date,
                NecessaryAttendance = this.NecessaryAttendance,
                RealAttendance = this.RealAttendance
            };
        }

        public StudentAttendance MapFromEntity(BusinessLogic.Entities.StudentAttendance studentAttendance)
        {
            Id = studentAttendance.Id;
            StudentId = studentAttendance.StudentId;
            SubjectId = studentAttendance.SubjectId;
            Date = studentAttendance.Date;
            NecessaryAttendance = studentAttendance.NecessaryAttendance;
            RealAttendance = studentAttendance.RealAttendance;
            return this;
        }
    }
}
