using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.Test.Model
{
    public class MockEmployee
    {
        public Employee GetEmployee()
        {
            var employee = new Employee()
            {
                Abbreviation = "Test",
                FirstName = "Vorname",
                LastName = "Nachname",
                City = "Zürich"
            };
            
            return employee;
        }
    }
}
