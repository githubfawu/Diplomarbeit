
using VZEintrittsApp.Model.Domain;

namespace VZEintrittsApp.Test.Model
{
    public static class MockSubsidiaryCompany
    {
        public static SubsidiaryCompany GetSubsidiaryCompany()
        {
            var subsidiaryCompany = new SubsidiaryCompany()
            {
                SubsidiaryCompanyId = 2,
                BranchNameForDescription = "HZ ZH",
                CompanyName = "HypothekenZentrum AG",
                Office = "Zürich",
                CityName = "Zürich",
                StateName = "Zürich",
                CountryCode = "CH",
                OfficialPhoneNumber = "+41 44 563 63 63",
                FaxNumber = "+41 44 563 63 64",
                PhoneNumberRangeLow = 41445636301,
                PhoneNumberRangeHigh = 41445636399,
                IsOutgoingNumberAnonymous = false
            };

            return subsidiaryCompany;
        }
    }
}
