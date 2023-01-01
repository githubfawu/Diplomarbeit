using System.Windows;
using Prism.Commands;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using Prism.Mvvm;
using System;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelGetNumber : BindableBase
    {
        private Repository Repository;
        public Employee CurrentEmployee { get; set; }
        public DelegateCommand GetFreeNumberCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public event EventHandler ClosingRequest;

        public ViewModelGetNumber(Employee employee, Repository repository)
        {
            CurrentEmployee = employee;
            Repository = repository;
            GetFreeNumberCommand = new DelegateCommand(GetFreeNumber);
            SaveCommand = new DelegateCommand(Save);
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

        private void Save()
        {
            Repository.WriteSpecificAdAttribute(nameof(CurrentEmployee.IpPhoneNumber), CurrentEmployee.Abbreviation, CurrentEmployee.IpPhoneNumber);
            Repository.WriteSpecificAdAttribute(nameof(CurrentEmployee.TelephoneNumber), CurrentEmployee.Abbreviation, CurrentEmployee.TelephoneNumber);
            OnClosingRequest();
        }
        protected void OnClosingRequest()
        {
            this.ClosingRequest(this, EventArgs.Empty);
        }
    }
}
