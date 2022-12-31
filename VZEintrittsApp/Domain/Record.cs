using System;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;
using VZEintrittsApp.Enums;

namespace VZEintrittsApp.Domain
{
    public class Record : BindableBase
    {
        [Key]
        public int employeeNr;
        public int EmployeeNr
        {
            get => employeeNr;
            set
            {
                if (value != employeeNr) SetProperty(ref employeeNr, value);
            }
        }

        public string? abbreviation;
        public string? Abbreviation
        {
            get => abbreviation;
            set
            {
                if (value != abbreviation) SetProperty(ref abbreviation, value);
            }
        }

        public string? associatedFile;
        public string? AssociatedFile
        {
            get => associatedFile;
            set
            {
                if (value != associatedFile) SetProperty(ref associatedFile, value);
            }
        }

        public string? recorder;
        public string? Recorder
        {
            get => recorder;
            set
            {
                if (value != recorder) SetProperty(ref recorder, value);
            }
        }

        public string? comment;
        public string? Comment
        {
            get => comment;
            set
            {
                if (value != comment) SetProperty(ref comment, value);
            }
        }

        public RecordStatus? status;
        public RecordStatus? Status
        {
            get => status;
            set
            {
                if (value != status) SetProperty(ref status, value);
            }
        }

        public DateTime? entryDate;
        public DateTime? EntryDate
        {
            get => entryDate;
            set
            {
                if (value != entryDate) SetProperty(ref entryDate, value);
            }
        }

        public DateTime? firstWorkingDay;
        public DateTime? FirstWorkingDay
        {
            get => firstWorkingDay;
            set
            {
                if (value != firstWorkingDay) SetProperty(ref firstWorkingDay, value);
            }
        }

        public DateTime? recordRead;
        public DateTime? RecordRead
        {
            get => recordRead;
            set
            {
                if (value != recordRead) SetProperty(ref recordRead, value);
            }
        }
    }
}
