
namespace VZEintrittsApp.Model.Employee
{
    public partial class Employee
    {
        private string? pager;
        public string? Pager
        {
            get => pager;
            set
            {
                if (value != pager) SetProperty(ref pager, value);
            }
        }

        private string? otherTelephone;
        public string? OtherTelephone
        {
            get => otherTelephone;
            set
            {
                if (value != otherTelephone) SetProperty(ref otherTelephone, value);
            }
        }

        private string? telephoneNumber;
        public string? TelephoneNumber
        {
            get => telephoneNumber;
            set
            {
                if (value != telephoneNumber) SetProperty(ref telephoneNumber, value);
            }
        }

        private string? ipPhoneNumber;
        public string? IpPhoneNumber
        {
            get => ipPhoneNumber;
            set
            {
                if (value != ipPhoneNumber) SetProperty(ref ipPhoneNumber, value);
            }
        }

        private string? faxNumber;
        public string? FaxNumber
        {
            get => faxNumber;
            set
            {
                if (value != faxNumber) SetProperty(ref faxNumber, value);
            }
        }
        private string? mobileNumber;
        public string? MobileNumber
        {
            get => mobileNumber;
            set
            {
                if (value != mobileNumber) SetProperty(ref mobileNumber, value);
            }
        }
    }
}
