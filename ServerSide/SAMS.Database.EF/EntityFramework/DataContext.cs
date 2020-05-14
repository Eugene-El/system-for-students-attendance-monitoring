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


        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
