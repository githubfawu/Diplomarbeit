using VZEintrittsApp.Domain;

namespace VZEintrittsApp.API.AD
{
    public class DirectoryServices
    {
        public DirectoryServices() { }

        public Employee GetAttributes(string abbreviation)
        {
            return new Employee()
            {
                Abbreviation = abbreviation,
                EmployeeNr = 3
            };
        }
    }
}
