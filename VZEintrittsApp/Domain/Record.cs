
using System;
using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    class Record : BindableBase
    {
        private int employeeNr;
        public int EmployeeNr
        {
            get => employeeNr;
            set => SetProperty(ref employeeNr, value);
        }

        private string abbreviation;
        public string Abbreviation
        {
            get => abbreviation;
            set => SetProperty(ref abbreviation, value);
        }

        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

    }
}
