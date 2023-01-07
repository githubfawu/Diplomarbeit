using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public class DirectReport : BindableBase
    {
        private string? samAccountName;
        public string? SamAccountName
        {
            get => samAccountName;
            set
            {
                if (value != samAccountName) SetProperty(ref samAccountName, value);
            }
        }

        private string? displayName;
        public string? DisplayName
        {
            get => displayName;
            set
            {
                if (value != displayName) SetProperty(ref displayName, value);
            }
        }

        private string? position;
        public string? Position
        {
            get => position;
            set
            {
                if (value != position) SetProperty(ref position, value);
            }
        }

        private string? startDate;
        public string? StartDate
        {
            get => startDate;
            set
            {
                if (value != startDate) SetProperty(ref startDate, value);
            }
        }
    }
}
