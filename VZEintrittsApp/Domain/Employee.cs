
namespace VZEintrittsApp.Domain
{
    public class Employee
    {
        private int employeeNr;
        public int EmployeeNr
        {
            get => employeeNr;
            set => employeeNr = value;
        }

        private string abbreviation;
        public string Abbreviation
        {
            get => abbreviation;
            set => abbreviation = value;
        }

    }
}
