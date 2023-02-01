
using VZEintrittsApp.Model.Domain;

namespace VZEintrittsApp.Model.EmployeeEntity
{
    public partial class Employee
    {
        private bool? titleInMailFooter;
        public bool? TitleInMailFooter
        {
            get => titleInMailFooter;
            set => SetProperty(ref titleInMailFooter, value);
        }

        private string vzAcademicTitle;
        public string? VzAcademicTitle
        {
            get => vzAcademicTitle;
            set => SetProperty(ref vzAcademicTitle, value);
        }

        private string vzPensum;
        public string? VzPensum
        {
            get => vzPensum;
            set => SetProperty(ref vzPensum, value);
        }

        private string vzBirthday;
        public string? VzBirthday
        {
            get => vzBirthday;
            set => SetProperty(ref vzBirthday, value);
        }

        private string vzStartDate;
        public string? VzStartDate
        {
            get => vzStartDate;
            set => SetProperty(ref vzStartDate, value);
        }

        private string vzGrade; //Rang
        public string? VzGrade
        {
            get => vzGrade;
            set => SetProperty(ref vzGrade, value);
        }

        private string vzBusinessUnitSupervisor;
        public string? VzBusinessUnitSupervisor
        {
            get => vzBusinessUnitSupervisor;
            set => SetProperty(ref vzBusinessUnitSupervisor, value);
        }

        private string vzRegionalSupervisor;
        public string? VzRegionalSupervisor
        {
            get => vzRegionalSupervisor;
            set => SetProperty(ref vzRegionalSupervisor, value);
        }

        private string vzTeam;
        public string? VzTeam
        {
            get => vzTeam;
            set => SetProperty(ref vzTeam, value);
        }

        private ManagementLevel? vzManagementLevel; //Kaderstufe
        public ManagementLevel? VzManagementLevel
        {
            get => vzManagementLevel;
            set => SetProperty(ref vzManagementLevel, value);
        }

        private string homePage;
        public string? HomePage
        {
            get => homePage;
            set => SetProperty(ref homePage, value);
        }

        private string extensionAttribute1;
        public string? ExtensionAttribute1
        {
            get => extensionAttribute1;
            set => SetProperty(ref extensionAttribute1, value);
        }

        private string extensionAttribute15;
        public string? ExtensionAttribute15
        {
            get => extensionAttribute15;
            set => SetProperty(ref extensionAttribute15, value);
        }
    }
}
