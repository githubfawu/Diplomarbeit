using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.Test.Model
{
    public static class MockEmployee
    {
        public static Employee GetEmployee()
        {
            var employee = new Employee()
            {
                Abbreviation = "TEst",
                FirstName = "FirstName",
                LastName = "LastName",
                CallSignName = "CallSignFirstName",
                CallSignLastName = "CallSignLastName",
                Company = "",
                Description = "HZ ZH",
                BusinessArea = "TestBusinessArea",
                TitleInMailFooter = false,
                VzAcademicTitle = "TestAcademicTitle",
                HomePage = ""
            };
            
            return employee;
        }
    }
}
