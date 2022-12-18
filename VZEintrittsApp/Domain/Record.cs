using System;
using System.ComponentModel.DataAnnotations;
using VZEintrittsApp.Enums;

namespace VZEintrittsApp.Domain
{
    public class Record
    {
        [Key]
        public int EmployeeNr { get; set; }

        public string Abbreviation { get; set; }

        public string AssociatedFile { get; set; }

        public RecordStatus Status { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
