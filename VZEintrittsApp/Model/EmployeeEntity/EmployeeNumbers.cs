
namespace VZEintrittsApp.Model.EmployeeEntity
{
    public partial class Employee
    {
        private string? pager;
        public string? Pager
        {
            get => pager;
            set => SetProperty(ref pager, value);
        }

        private string? otherTelephone;
        public string? OtherTelephone
        {
            get => otherTelephone;
            set => SetProperty(ref otherTelephone, value);
        }

        private string? telephoneNumber;
        public string? TelephoneNumber
        {
            get => telephoneNumber;
            set => SetProperty(ref telephoneNumber, value);
        }

        private string? ipPhoneNumber;
        public string? IpPhoneNumber
        {
            get => ipPhoneNumber;
            set => SetProperty(ref ipPhoneNumber, value);
        }

        private string? faxNumber;
        public string? FaxNumber
        {
            get => faxNumber;
            set => SetProperty(ref faxNumber, value);
        }
        private string? mobileNumber;
        public string? MobileNumber
        {
            get => mobileNumber;
            set => SetProperty(ref mobileNumber, value);
        }
    }
}
