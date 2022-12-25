using Microsoft.EntityFrameworkCore;
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
        public DbSet<StateAndCountry> StatesAndCountries { get; set; }
        public DbSet<BranchAndPhone> BranchAndPhones { get; set; }
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

            modelBuilder.Entity<StateAndCountry>()
                .HasKey(p => p.CityId);

            modelBuilder.Entity<BranchAndPhone>()
                .HasKey(p => p.BranchId);

            modelBuilder.Entity<StateAndCountry>().HasData(StateCountrySeeder.GetSeeds());
            modelBuilder.Entity<BranchAndPhone>().HasData(BranchAndPhoneSeeder.GetSeeds());
        }
    }
}
