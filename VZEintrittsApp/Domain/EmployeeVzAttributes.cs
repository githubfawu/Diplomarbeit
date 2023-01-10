
namespace VZEintrittsApp.Domain
{
    public partial class Employee
    {
        private bool? titleInMailFooter;
        public bool? TitleInMailFooter
        {
            get => titleInMailFooter;
            set
            {
                if (value != titleInMailFooter) SetProperty(ref titleInMailFooter, value);
            }
        }

        private string vzAcademicTitle;
        public string? VzAcademicTitle
        {
            get => vzAcademicTitle;
            set
            {
                if (value != vzAcademicTitle) SetProperty(ref vzAcademicTitle, value);
            }
        }

        private string vzPensum;
        public string? VzPensum
        {
            get => vzPensum;
            set
            {
                if (value != vzPensum) SetProperty(ref vzPensum, value);
            }
        }

        private string vzBirthday;
        public string? VzBirthday
        {
            get => vzBirthday;
            set
            {
                if (value != vzBirthday) SetProperty(ref vzBirthday, value);
            }
        }

        private string vzStartDate;
        public string? VzStartDate
        {
            get => vzStartDate;
            set
            {
                if (value != vzStartDate) SetProperty(ref vzStartDate, value);
            }
        }

        private string vzGrade; //Rang
        public string? VzGrade
        {
            get => vzGrade;
            set
            {
                if (value != vzGrade) SetProperty(ref vzGrade, value);
            }
        }

        private string vzBusinessUnitSupervisor;
        public string? VzBusinessUnitSupervisor
        {
            get => vzBusinessUnitSupervisor;
            set
            {
                if (value != vzBusinessUnitSupervisor) SetProperty(ref vzBusinessUnitSupervisor, value);
            }
        }

        private string vzRegionalSupervisor;
        public string? VzRegionalSupervisor
        {
            get => vzRegionalSupervisor;
            set
            {
                if (value != vzRegionalSupervisor) SetProperty(ref vzRegionalSupervisor, value);
            }
        }

        private ManagementLevel? vzManagementLevel; //Kaderstufe
        public ManagementLevel? VzManagementLevel
        {
            get => vzManagementLevel;
            set
            {
                if (value != vzManagementLevel) SetProperty(ref vzManagementLevel, value);
            }
        }

        private string homePage;
        public string? HomePage
        {
            get => homePage;
            set
            {
                if (value != homePage) SetProperty(ref homePage, value);
            }
        }
    }
}
