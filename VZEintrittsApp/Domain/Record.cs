using System;
using System.ComponentModel.DataAnnotations;
using VZEintrittsApp.Enums;

namespace VZEintrittsApp.Domain
{
    public class Record
    {
        private int employeeNr;
        [Key]
        public int EmployeeNr
        {
            get => employeeNr;
            set => employeeNr = value;
        }

        private string abbreviation;
        public string Abbreviation
        {
            get => abbreviation;
            set => abbreviation = value;
        }

        private string associatedFile;
        public string AssociatedFile
        {
            get => associatedFile;
            set => associatedFile = value;
        }

        private RecordStatus status;
        public RecordStatus Status
        {
            get => status;
            set => status = value;
        }

        private DateTime entryDate;
        public DateTime EntryDate
        {
            get => entryDate;
            set => entryDate = value;
        }

    }
}
