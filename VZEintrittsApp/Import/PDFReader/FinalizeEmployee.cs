using System.Collections.Generic;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;

namespace VZEintrittsApp.Import.PDFReader
{
    public class FinalizeEmployee
    {
        private ContextHelper ContextHelper { get; set; }

        public FinalizeEmployee(ContextHelper contextHelper)
        {
            this.ContextHelper = contextHelper;
        }
        public List<Employee> FinalizeEmployees(List<Employee> employeeList)
        {
            employeeList = CheckForCallSigns(employeeList);
            employeeList = AddStateAndCountry(employeeList);

            return employeeList;
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
    }
}
