﻿
using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public class Employee : BindableBase
    {
        public int? EmployeeNr { get; set; }
        public string? Abbreviation { get; set; }
        public string? NameOfAdObject { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CallSignName { get; set; }
        public string? CallSignLastName { get; set; }
        public string? MailAdress { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? BusinessArea { get; set; }
        public string? Company { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Office { get; set; }
        public string? Description { get; set; }
        public string? PostOfficeBox { get; set; }
        public string? Country { get; set; }
        public string? Pager { get; set; }
        public string? OtherTelephone { get; set; }
        public string? TelephoneNumber { get; set; }

        private string? ipPhoneNumber;
        public string? IpPhoneNumber
        {
            get => ipPhoneNumber;
            set
            {
                if (value != ipPhoneNumber)
                {
                    SetProperty(ref ipPhoneNumber, value);
                }
            }
        }
        public string? FaxNumber { get; set; }
        public bool? TitleInMailFooter { get; set; }
        public string? VzAcademicTitle { get; set; }
        public string? VzPensum { get; set; }
        public string? VzBirthday { get; set; }
        public string? VzStartDate { get; set; }
        public string? VzGrade { get; set; }//Rang
        public string? VzBusinessUnitSupervisor { get; set; }
        public string? VzRegionalSupervisor { get; set; }
        public string? VzManagementLevel { get; set; }//Kaderstufe

    }
}
