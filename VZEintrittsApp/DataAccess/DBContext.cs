using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public DbSet<AttributeNotations> AttributeNotations { get; set; }
        public DbSet<NumberFormat> NumberFormats { get; set; }
        public DbSet<ManagementLevel> ManagementLevels { get; set; }
        public DbSet<RecordStatus> RecordStatus { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasKey(p => p.RecordId);

            modelBuilder.Entity<RecordStatus>()
                .HasKey(p => p.RecordStatusId);
            modelBuilder.Entity<RecordStatus>().HasData(RecordStatusSeeder.GetSeeds());

            modelBuilder.Entity<SavedFile>()
                .HasKey(p => p.FileName);

            modelBuilder.Entity<Log>()
                .HasKey(p => p.LogNr);

            modelBuilder.Entity<SubsidiaryCompany>()
                .HasKey(p => p.SubsidiaryCompanyId);
            modelBuilder.Entity<SubsidiaryCompany>().HasData(SubsidiaryCompanySeeder.GetSeeds());

            modelBuilder.Entity<AttributeNotations>()
                .HasKey(p => p.NotationId);
            modelBuilder.Entity<AttributeNotations>().HasData(AttributeNotationsSeeder.GetSeeds());

            modelBuilder.Entity<ManagementLevel>()
                .HasKey(p => p.MgmtLevelId);
            modelBuilder.Entity<ManagementLevel>().HasData(ManagementLevelSeeder.GetSeeds());

            modelBuilder.Entity<NumberFormat>()
                .Property(b => b.Formats)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<int, string>>(v));
            modelBuilder.Entity<NumberFormat>().HasData(NumberFormatSeeder.GetSeeds());

        }
    }
}
