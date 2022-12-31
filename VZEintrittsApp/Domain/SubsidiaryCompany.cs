
namespace VZEintrittsApp.Domain
{
    public class SubsidiaryCompany
    {
        public int SubsidiaryCompanyId { get; set; }
        public string BranchNameForDescription { get; set; }
        public string CompanyName { get; set; }
        public string Office { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryCode { get; set; }
        public string OfficialPhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public long PhoneNumberRangeLow { get; set; }
        public long PhoneNumberRangeHigh { get; set; }

    }
}
