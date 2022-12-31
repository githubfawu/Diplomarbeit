
namespace VZEintrittsApp.Domain
{
    public partial class Employee
    {
        public bool? titleInMailFooter;
        public bool? TitleInMailFooter
        {
            get => titleInMailFooter;
            set
            {
                if (value != titleInMailFooter) SetProperty(ref titleInMailFooter, value);
            }
        }

        public string vzAcademicTitle;
        public string? VzAcademicTitle
        {
            get => vzAcademicTitle;
            set
            {
                if (value != vzAcademicTitle) SetProperty(ref vzAcademicTitle, value);
            }
        }

        public string vzPensum;
        public string? VzPensum
        {
            get => vzPensum;
            set
            {
                if (value != vzPensum) SetProperty(ref vzPensum, value);
            }
        }

        public string vzBirthday;
        public string? VzBirthday
        {
            get => vzBirthday;
            set
            {
                if (value != vzBirthday) SetProperty(ref vzBirthday, value);
            }
        }

        public string vzStartDate;
        public string? VzStartDate
        {
            get => vzStartDate;
            set
            {
                if (value != vzStartDate) SetProperty(ref vzStartDate, value);
            }
        }

        public string vzGrade; //Rang
        public string? VzGrade
        {
            get => vzGrade;
            set
            {
                if (value != vzGrade) SetProperty(ref vzGrade, value);
            }
        }

        public string vzBusinessUnitSupervisor;
        public string? VzBusinessUnitSupervisor
        {
            get => vzBusinessUnitSupervisor;
            set
            {
                if (value != vzBusinessUnitSupervisor) SetProperty(ref vzBusinessUnitSupervisor, value);
            }
        }

        public string vzRegionalSupervisor;
        public string? VzRegionalSupervisor
        {
            get => vzRegionalSupervisor;
            set
            {
                if (value != vzRegionalSupervisor) SetProperty(ref vzRegionalSupervisor, value);
            }
        }

        public string vzManagementLevel; //Kaderstufe
        public string? VzManagementLevel
        {
            get => vzManagementLevel;
            set
            {
                if (value != vzManagementLevel) SetProperty(ref vzManagementLevel, value);
            }
        }
    }
}
