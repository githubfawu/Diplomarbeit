using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import.PDFReader
{
    public class FinalizeEmployee
    {
        private Employee Employee { get; set; }
        public Employee FinalizeEmployees(Employee employee)
        {
            Employee = employee;
            CheckForCallSigns(Employee);
            CheckForEmptyCompany(Employee);
            CheckForMailfooter(Employee);

            return Employee;
        }

        private Employee CheckForCallSigns(Employee employee)
        {
            if (employee.CallSignName != null)
            {
                employee.FirstName = employee.CallSignName;

            }
            if (employee.CallSignLastName != null)
            {
                employee.LastName = employee.CallSignLastName;
            }

            return employee;
        }

        private Employee CheckForEmptyCompany(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Company))
            {
                employee.Company = employee.BusinessArea;
            }
            return employee;
        }

        private Employee CheckForMailfooter(Employee employee)
        {
            if (employee.TitleInMailFooter == false)
            {
                employee.VzAcademicTitle = null;
            }
            return employee;
        }
    }
}
