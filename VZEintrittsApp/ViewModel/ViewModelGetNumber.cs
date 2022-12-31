using Prism.Commands;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using Prism.Mvvm;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelGetNumber : BindableBase
    {
        private Repository Repository;
        public Employee CurrentEmployee { get; set; }
        public DelegateCommand GetFreeNumberCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public ViewModelGetNumber(Employee employee, Repository repository)
        {
            CurrentEmployee = employee;
            Repository = repository;
            GetFreeNumberCommand = new DelegateCommand(GetFreeNumber);
        }

        private bool showLabelDescription = false;
        public bool ShowLabelDescription
        {
            get => showLabelDescription;
            set => SetProperty(ref showLabelDescription, value);
        }

        private void GetFreeNumber()
        {
            if (!string.IsNullOrWhiteSpace(CurrentEmployee.Description))
            {
                var numbers = Repository.GetFreeNumberFromAd(CurrentEmployee.Description);
                CurrentEmployee.IpPhoneNumber = numbers[0];
                CurrentEmployee.TelephoneNumber = numbers[1];
            }
            else
            {
                ShowLabelDescription = true;
            }
        }
    }
}
