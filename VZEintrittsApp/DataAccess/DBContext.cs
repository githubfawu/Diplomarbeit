using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class DBContext : DbContext
    {
        public string DbPath { get; }
        public DBContext()
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
