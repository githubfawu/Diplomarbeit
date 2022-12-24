using System.Collections.Generic;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;

namespace VZEintrittsApp.Import.PDFReader
{
    public class FinalizeEmployee
    {
        private ContextHelper ContextHelper { get; set; }
        private List<Employee> EmployeeList { get; set; }

        public FinalizeEmployee(ContextHelper contextHelper, List<Employee> employeeList)
        {
            this.ContextHelper = contextHelper;
            EmployeeList = employeeList;
        }
        public List<Employee> FinalizeEmployees()
        {
            CheckForCallSigns(EmployeeList);
            CheckForCompany(EmployeeList);
            AddStateAndCountry(EmployeeList);
            CheckForMailfooter(EmployeeList);

            return EmployeeList;
        }

        private List<Employee> CheckForCallSigns(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                if (employee.CallSignName != null)
                {
                    employee.FirstName = employee.CallSignName;

                }
                if (employee.CallSignLastName != null)
                {
                    employee.LastName = employee.CallSignLastName;
                }
            }

            return employeeList;
        }

        private List<Employee> CheckForCompany(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                if (string.IsNullOrWhiteSpace(employee.Company))
                {
                    employee.Company = employee.BusinessArea;
                }
            }

            return employeeList;
        }

        private List<Employee> AddStateAndCountry(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                if (employee.City != null)
                {
                    var stateAndCountry = ContextHelper.GetStateAndCountry(employee.City);
                    employee.State = stateAndCountry.StateName;
                    employee.Country = stateAndCountry.CountryCode;
                }
            }

            return employeeList;
        }

        private List<Employee> CheckForMailfooter(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                if (employee.TitleInMailFooter == false)
                {
                    employee.VzAcademicTitle = null;
                }
            }

            return employeeList;
        }
    }
}
