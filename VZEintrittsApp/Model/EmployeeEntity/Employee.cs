using System;
using Prism.Mvvm;

namespace VZEintrittsApp.Model.EmployeeEntity
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
            set => SetProperty(ref employeeNr, value);
        }

        private string? abbreviation;
        public string? Abbreviation
        {
            get => abbreviation;
            set => SetProperty(ref abbreviation, value);
        }

        private string? nameOfAdObject;
        public string? NameOfAdObject
        {
            get => nameOfAdObject;
            set => SetProperty(ref nameOfAdObject, value);
        }

        private string? displayName;
        public string? DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }

        private string? firstName;
        public string? FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string? lastName;
        public string? LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string? callSignName;
        public string? CallSignName
        {
            get => callSignName;
            set => SetProperty(ref callSignName, value);
        }

        private string? callSignLastName;
        public string? CallSignLastName
        {
            get => callSignLastName;
            set => SetProperty(ref callSignLastName, value);
        }

        private string? mailAdress;
        public string? MailAdress
        {
            get => mailAdress;
            set => SetProperty(ref mailAdress, value);
        }

        private string? department;
        public string? Department
        {
            get => department;
            set => SetProperty(ref department, value);
        }

        private string? position;
        public string? Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        private string? businessArea;
        public string? BusinessArea
        {
            get => businessArea;
            set => SetProperty(ref businessArea, value);
        }

        private string? company;
        public string? Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }

        private string? street;
        public string? Street
        {
            get => street;
            set => SetProperty(ref street, value);
        }

        private string? city;
        public string? City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        private string? state;
        public string? State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        private string? postalCode;
        public string? PostalCode
        {
            get => postalCode;
            set => SetProperty(ref postalCode, value);
        }

        private string? office;
        public string? Office
        {
            get => office;
            set => SetProperty(ref office, value);
        }

        private string? description;
        public string? Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string? postOfficeBox;
        public string? PostOfficeBox
        {
            get => postOfficeBox;
            set => SetProperty(ref postOfficeBox, value);
        }

        private string? country;
        public string? Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        private string? manager;
        public string? Manager
        {
            get => manager;
            set => SetProperty(ref manager, value);
        }

        private DateTime? expirationDate;
        public DateTime? ExpirationDate
        {
            get => expirationDate;
            set => SetProperty(ref expirationDate, value);
        }
    }
}
