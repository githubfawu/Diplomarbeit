using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import.PDFReader
{
    public class AddIndividualProperties
    {
        public Employee AddProperties(Employee employee, SubsidiaryCompany subsidiaryCompany)
        {
            if (subsidiaryCompany != null && employee != null)
            {
                employee.State = subsidiaryCompany.StateName;
                employee.Country = subsidiaryCompany.CountryCode;
                employee.Office = subsidiaryCompany.Office;
                employee.Description = subsidiaryCompany.BranchNameForDescription;
                employee.Pager = subsidiaryCompany.IsOutgoingNumberAnonymous ? "ANONYMOUS" : subsidiaryCompany.OfficialPhoneNumber;
                employee.OtherTelephone = subsidiaryCompany.OfficialPhoneNumber;
                employee.FaxNumber = subsidiaryCompany.FaxNumber;
            }
            return employee;
        }
    }
}
