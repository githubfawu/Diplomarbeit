using Microsoft.EntityFrameworkCore;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public string DbPath { get; }
        public DbContext()
        {
            DbPath = System.IO.Path.GetFullPath("C:\\Temp\\Eintritte.db");
        }
        public DbSet<Record> Records { get; set; }
        public DbSet<SavedFile> SavedFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasKey(p => p.EmployeeNr);

            modelBuilder.Entity<SavedFile>()
                .HasKey(p => p.FileName);
        }
    }
}
