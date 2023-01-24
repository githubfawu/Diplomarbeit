using System;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;

namespace VZEintrittsApp.Model.RecordEntity
{
    public class Record : BindableBase
    {
        [Key]
        private int recordId;
        public int RecordId
        {
            get => recordId;
            set
            {
                if (value != recordId) SetProperty(ref recordId, value);
            }
        }

        private int employeeNr;
        public int EmployeeNr
        {
            get => employeeNr;
            set
            {
                if (value != employeeNr) SetProperty(ref employeeNr, value);
            }
        }

        private string? abbreviation;
        public string? Abbreviation
        {
            get => abbreviation;
            set
            {
                if (value != abbreviation) SetProperty(ref abbreviation, value);
            }
        }

        private string? associatedFile;
        public string? AssociatedFile
        {
            get => associatedFile;
            set
            {
                if (value != associatedFile) SetProperty(ref associatedFile, value);
            }
        }

        private string? recorder;
        public string? Recorder
        {
            get => recorder;
            set
            {
                if (value != recorder) SetProperty(ref recorder, value);
            }
        }

        private string? comment;
        public string? Comment
        {
            get => comment;
            set
            {
                if (value != comment) SetProperty(ref comment, value);
            }
        }

        private RecordStatus? status;
        public RecordStatus? Status
        {
            get => status;
            set
            {
                if (value != status) SetProperty(ref status, value);
            }
        }

        private DateTime? entryDate;
        public DateTime? EntryDate
        {
            get => entryDate;
            set
            {
                if (value != entryDate) SetProperty(ref entryDate, value);
            }
        }

        private DateTime? firstWorkingDay;
        public DateTime? FirstWorkingDay
        {
            get => firstWorkingDay;
            set
            {
                if (value != firstWorkingDay) SetProperty(ref firstWorkingDay, value);
            }
        }

        private DateTime? recordReadDate;
        public DateTime? RecordReadDate
        {
            get => recordReadDate;
            set
            {
                if (value != recordReadDate) SetProperty(ref recordReadDate, value);
            }
        }
    }
}
