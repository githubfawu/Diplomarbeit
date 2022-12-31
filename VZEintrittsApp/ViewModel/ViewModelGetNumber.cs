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

        private void GetFreeNumber()
        {
            if (!string.IsNullOrWhiteSpace(CurrentEmployee.Description))
            {
                var phoneNumber = Repository.GetFreeNumberFromAd(CurrentEmployee.Description);
                CurrentEmployee.TelephoneNumber = phoneNumber;//Korrektes Format +41 56 621 etc...
                CurrentEmployee.IpPhoneNumber = phoneNumber;
            }

        }
    }
}
