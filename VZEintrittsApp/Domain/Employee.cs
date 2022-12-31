using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public partial class Employee : BindableBase
    {
        public int? employeeNr;
        public int? EmployeeNr
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

        public string? nameOfAdObject;
        public string? NameOfAdObject
        {
            get => nameOfAdObject;
            set
            {
                if (value != nameOfAdObject) SetProperty(ref nameOfAdObject, value);
            }
        }

        public string? displayName;
        public string? DisplayName
        {
            get => displayName;
            set
            {
                if (value != displayName) SetProperty(ref displayName, value);
            }
        }

        public string? firstName;
        public string? FirstName
        {
            get => firstName;
            set
            {
                if (value != firstName) SetProperty(ref firstName, value);
            }
        }

        public string? lastName;
        public string? LastName
        {
            get => lastName;
            set
            {
                if (value != lastName) SetProperty(ref lastName, value);
            }
        }

        public string? callSignName;
        public string? CallSignName
        {
            get => callSignName;
            set
            {
                if (value != callSignName) SetProperty(ref callSignName, value);
            }
        }

        public string? callSignLastName;
        public string? CallSignLastName
        {
            get => callSignLastName;
            set
            {
                if (value != callSignLastName) SetProperty(ref callSignLastName, value);
            }
        }

        public string? mailAdress;
        public string? MailAdress
        {
            get => mailAdress;
            set
            {
                if (value != mailAdress) SetProperty(ref mailAdress, value);
            }
        }

        public string? department;
        public string? Department
        {
            get => department;
            set
            {
                if (value != department) SetProperty(ref department, value);
            }
        }

        public string? position;
        public string? Position
        {
            get => position;
            set
            {
                if (value != position) SetProperty(ref position, value);
            }
        }

        public string? businessArea;
        public string? BusinessArea
        {
            get => businessArea;
            set
            {
                if (value != businessArea) SetProperty(ref businessArea, value);
            }
        }

        public string? company;
        public string? Company
        {
            get => company;
            set
            {
                if (value != company) SetProperty(ref company, value);
            }
        }

        public string? street;
        public string? Street
        {
            get => street;
            set
            {
                if (value != street) SetProperty(ref street, value);
            }
        }

        public string? city;
        public string? City
        {
            get => city;
            set
            {
                if (value != city) SetProperty(ref city, value);
            }
        }

        public string? state;
        public string? State
        {
            get => state;
            set
            {
                if (value != state) SetProperty(ref state, value);
            }
        }

        public string? postalCode;
        public string? PostalCode
        {
            get => postalCode;
            set
            {
                if (value != postalCode) SetProperty(ref postalCode, value);
            }
        }

        public string? office;
        public string? Office
        {
            get => office;
            set
            {
                if (value != office) SetProperty(ref office, value);
            }
        }

        public string? description;
        public string? Description
        {
            get => description;
            set
            {
                if (value != description) SetProperty(ref description, value);
            }
        }

        public string? postOfficeBox;
        public string? PostOfficeBox
        {
            get => postOfficeBox;
            set
            {
                if (value != postOfficeBox) SetProperty(ref postOfficeBox, value);
            }
        }

        public string? country;
        public string? Country
        {
            get => country;
            set
            {
                if (value != country) SetProperty(ref country, value);
            }
        }
    }
}
