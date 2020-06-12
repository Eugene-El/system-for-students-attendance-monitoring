using Microsoft.EntityFrameworkCore;
using SAMS.Database.EF.EntitiesDb;

namespace SAMS.Database.EF.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<StudyProgramme> StudyProgrammes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<NotificationRule> NotificationRules { get; set; }
        public DbSet<NotificationHistory> NotificationHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Faculty>()
                .Property(p => p.Code)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<Faculty>()
                .Property(p => p.TitleEn)
                .IsUnicode(false).HasMaxLength(255);
            builder.Entity<Faculty>()
                .Property(p => p.TitleLv)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Faculty>()
                .Property(p => p.TitleRu)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Faculty>()
                .Property(p => p.ShortTitleEn)
                .IsUnicode(false).HasMaxLength(64);
            builder.Entity<Faculty>()
                .Property(p => p.ShortTitleLv)
                .IsUnicode(true).HasMaxLength(64);
            builder.Entity<Faculty>()
                .Property(p => p.ShortTitleRu)
                .IsUnicode(true).HasMaxLength(64);

            builder.Entity<StudyProgramme>()
                .Property(p => p.Code)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<StudyProgramme>()
                .Property(p => p.TitleEn)
                .IsUnicode(false).HasMaxLength(255);
            builder.Entity<StudyProgramme>()
                .Property(p => p.TitleLv)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<StudyProgramme>()
                .Property(p => p.TitleRu)
                .IsUnicode(true).HasMaxLength(255);


            builder.Entity<Subject>()
                .Property(p => p.Code)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<Subject>()
                .Property(p => p.TitleEn)
                .IsUnicode(false).HasMaxLength(255);
            builder.Entity<Subject>()
                .Property(p => p.TitleLv)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Subject>()
                .Property(p => p.TitleRu)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Subject>()
                .Property(p => p.ShortTitleEn)
                .IsUnicode(false).HasMaxLength(64);
            builder.Entity<Subject>()
                .Property(p => p.ShortTitleLv)
                .IsUnicode(true).HasMaxLength(64);
            builder.Entity<Subject>()
                .Property(p => p.ShortTitleRu)
                .IsUnicode(true).HasMaxLength(64);


            builder.Entity<Student>()
                .Property(p => p.Code)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<Student>()
                .Property(p => p.Name)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Student>()
                .Property(p => p.Surname)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Student>()
                .Property(p => p.Phone1)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<Student>()
                .Property(p => p.Phone2)
                .IsUnicode(false).HasMaxLength(32);
            builder.Entity<Student>()
                .Property(p => p.Email1)
                .IsUnicode(false).HasMaxLength(255);
            builder.Entity<Student>()
                .Property(p => p.Email2)
                .IsUnicode(false).HasMaxLength(255);
            builder.Entity<Student>()
                .Property(p => p.Skype)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<Student>()
                .Property(p => p.Comment)
                .IsUnicode(true).HasMaxLength(1024);

            builder.Entity<StudentAttendance>()
                .Property(p => p.Date)
                .HasColumnType("DATE");

            builder.Entity<Configuration>()
                .Property(p => p.Content)
                .IsUnicode(true).HasMaxLength(2048);

            builder.Entity<NotificationRule>()
                .Property(p => p.Name)
                .IsUnicode(true).HasMaxLength(255);
            builder.Entity<NotificationRule>()
                .Property(p => p.Message)
                .IsUnicode(true).HasMaxLength(2048);


            builder.Entity<NotificationHistory>()
                .Property(p => p.Message)
                .IsUnicode(true).HasMaxLength(2048);
            builder.Entity<NotificationHistory>()
                .Property(p => p.ErrorMessage)
                .IsUnicode(true).HasMaxLength(1024);

        }
    }
}
