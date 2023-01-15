using System.Windows;
using Prism.Commands;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

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
            SubsidiaryCompanies = Repository.GetAllSubsidiaries();
            GetFreeNumberCommand = new DelegateCommand(GetFreeNumber);
            SaveCommand = new DelegateCommand(Save);
        }

        private bool showLabelDescription = false;
        public bool ShowLabelDescription
        {
            get => showLabelDescription;
            set => SetProperty(ref showLabelDescription, value);
        }

        private ObservableCollection<SubsidiaryCompany> subsidiaryCompanies;
        public ObservableCollection<SubsidiaryCompany> SubsidiaryCompanies
        {
            get { return subsidiaryCompanies; }
            set { subsidiaryCompanies = value; }
        }

        private SubsidiaryCompany selectedCompany;
        public SubsidiaryCompany SelectedCompany
        {
            get { return selectedCompany; }
            set { selectedCompany = value;
                CurrentEmployee.Description = selectedCompany.BranchNameForDescription;
                ShowLabelDescription = false;
            }
        }

        private void GetFreeNumber()
        {
            if (!string.IsNullOrWhiteSpace(CurrentEmployee.Description))
            {
                var numbers = Repository.GetFreeNumberFromAd(CurrentEmployee.Description);
                if (numbers != null)
                {
                    CurrentEmployee.IpPhoneNumber = numbers[0];
                    CurrentEmployee.TelephoneNumber = numbers[1];
                    CurrentEmployee.Pager = numbers[2];
                    
                }
            }
            else
            {
                ShowLabelDescription = true;
            }
        }

        private void Save()
        {
            Repository.WriteSpecificAdAttribute(nameof(CurrentEmployee.IpPhoneNumber), CurrentEmployee.IpPhoneNumber, CurrentEmployee);
            Repository.WriteSpecificAdAttribute(nameof(CurrentEmployee.TelephoneNumber), CurrentEmployee.TelephoneNumber, CurrentEmployee);
            Repository.WriteSpecificAdAttribute(nameof(CurrentEmployee.Pager), CurrentEmployee.Pager, CurrentEmployee);
            OnClosingRequest();
        }
        protected void OnClosingRequest()
        {
            this.ClosingRequest(this, EventArgs.Empty);
        }
    }
}
