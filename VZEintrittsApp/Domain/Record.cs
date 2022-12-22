using System;
using System.ComponentModel.DataAnnotations;
using VZEintrittsApp.Enums;

namespace VZEintrittsApp.Domain
{
    public class Record
    {
        [Key]
        public int EmployeeNr { get; set; }
        public string? Abbreviation { get; set; }
        public string? AssociatedFile { get; set; }
        public string? Recorder { get; set; }
        public RecordStatus Status { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime FirstWorkingDay { get; set; }
        public DateTime RecordRead { get; set; }

    }
}
