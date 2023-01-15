using System;
using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public partial class Employee : BindableBase, ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        private int? employeeNr;
        public int? EmployeeNr
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

        private string? nameOfAdObject;
        public string? NameOfAdObject
        {
            get => nameOfAdObject;
            set
            {
                if (value != nameOfAdObject) SetProperty(ref nameOfAdObject, value);
            }
        }

        private string? displayName;
        public string? DisplayName
        {
            get => displayName;
            set
            {
                if (value != displayName) SetProperty(ref displayName, value);
            }
        }

        private string? firstName;
        public string? FirstName
        {
            get => firstName;
            set
            {
                if (value != firstName) SetProperty(ref firstName, value);
            }
        }

        private string? lastName;
        public string? LastName
        {
            get => lastName;
            set
            {
                if (value != lastName) SetProperty(ref lastName, value);
            }
        }

        private string? callSignName;
        public string? CallSignName
        {
            get => callSignName;
            set
            {
                if (value != callSignName) SetProperty(ref callSignName, value);
            }
        }

        private string? callSignLastName;
        public string? CallSignLastName
        {
            get => callSignLastName;
            set
            {
                if (value != callSignLastName) SetProperty(ref callSignLastName, value);
            }
        }

        private string? mailAdress;
        public string? MailAdress
        {
            get => mailAdress;
            set
            {
                if (value != mailAdress) SetProperty(ref mailAdress, value);
            }
        }

        private string? department;
        public string? Department
        {
            get => department;
            set
            {
                if (value != department) SetProperty(ref department, value);
            }
        }

        private string? position;
        public string? Position
        {
            get => position;
            set
            {
                if (value != position) SetProperty(ref position, value);
            }
        }

        private string? businessArea;
        public string? BusinessArea
        {
            get => businessArea;
            set
            {
                if (value != businessArea) SetProperty(ref businessArea, value);
            }
        }

        private string? company;
        public string? Company
        {
            get => company;
            set
            {
                if (value != company) SetProperty(ref company, value);
            }
        }

        private string? street;
        public string? Street
        {
            get => street;
            set
            {
                if (value != street) SetProperty(ref street, value);
            }
        }

        private string? city;
        public string? City
        {
            get => city;
            set
            {
                if (value != city) SetProperty(ref city, value);
            }
        }

        private string? state;
        public string? State
        {
            get => state;
            set
            {
                if (value != state) SetProperty(ref state, value);
            }
        }

        private string? postalCode;
        public string? PostalCode
        {
            get => postalCode;
            set
            {
                if (value != postalCode) SetProperty(ref postalCode, value);
            }
        }

        private string? office;
        public string? Office
        {
            get => office;
            set
            {
                if (value != office) SetProperty(ref office, value);
            }
        }

        private string? description;
        public string? Description
        {
            get => description;
            set
            {
                if (value != description) SetProperty(ref description, value);
            }
        }

        private string? postOfficeBox;
        public string? PostOfficeBox
        {
            get => postOfficeBox;
            set
            {
                if (value != postOfficeBox) SetProperty(ref postOfficeBox, value);
            }
        }

        private string? country;
        public string? Country
        {
            get => country;
            set
            {
                if (value != country) SetProperty(ref country, value);
            }
        }

        private string? manager;
        public string? Manager
        {
            get => manager;
            set
            {
                if (value != manager) SetProperty(ref manager, value);
            }
        }

        private DateTime? expirationDate;
        public DateTime? ExpirationDate
        {
            get => expirationDate;
            set
            {
                if (value != expirationDate) SetProperty(ref expirationDate, value);
            }
        }
    }
}
