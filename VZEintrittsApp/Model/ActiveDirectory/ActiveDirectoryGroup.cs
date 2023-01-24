using Prism.Mvvm;

namespace VZEintrittsApp.Model.ActiveDirectory
{
    public class ActiveDirectoryGroup : BindableBase
    {
        private string? adGroupName;
        public string? AdGroupName
        {
            get => adGroupName;
            set
            {
                if (value != adGroupName) SetProperty(ref adGroupName, value);
            }
        }

        private string? adGroupDescription;
        public string? AdGroupDescription
        {
            get => adGroupDescription;
            set
            {
                if (value != adGroupDescription) SetProperty(ref adGroupDescription, value);
            }
        }

    }
}
