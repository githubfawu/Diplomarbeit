using Microsoft.EntityFrameworkCore;
using VZEintrittsApp.DataAccess.Seeders;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Logger;

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
        public DbSet<SubsidiaryCompany> SubsidiaryCompanies { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasKey(p => p.EmployeeNr);

            modelBuilder.Entity<SavedFile>()
                .HasKey(p => p.FileName);

            modelBuilder.Entity<Log>()
                .HasKey(p => p.LogNr);

            modelBuilder.Entity<SubsidiaryCompany>()
                .HasKey(p => p.SubsidiaryCompanyId);

            modelBuilder.Entity<SubsidiaryCompany>().HasData(SubsidiaryCompanySeeder.GetSeeds());
        }
    }
}
