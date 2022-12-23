using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Enums;
using VZEintrittsApp.Model;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        private bool isProgressBarVisible = false;
        public bool IsProgressBarVisible
        {
            get => isProgressBarVisible;
            set => SetProperty(ref isProgressBarVisible, value);
        }

        private double progressValue;
        public double ProgressValue
        {
            get
            {
                return progressValue;
            }
            set => SetProperty(ref progressValue, value);
        }

        private string progressText;
        public string ProgressText
        {
            get
            {
                return progressText;
            }
            set => SetProperty(ref progressText, value);
        }
        public DelegateCommand UpdateCommand { get; set; }
        private Repository Repository { get; set; }


        private ObservableCollection<Record> recordsList = new ObservableCollection<Record>();
        public ObservableCollection<Record> RecordsList
        {
            get => recordsList;
            set
            {
                if (value != recordsList)
                {
                    SetProperty(ref recordsList, value);
                }
            }
        }

        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set
            {
                if (value != currentEmployee)
                {
                    SetProperty(ref currentEmployee, value);
                }
            }
        }

        private Record selectedItem;
        public Record SelectedItem
        {
            get
            {
                if (selectedItem == null)
                {
                    return null;
                }
                Repository.GetOriginalDocument(selectedItem.AssociatedFile);
                OpenWithDefaultProgram();
                IsProgressBarVisible = true;
                ProgressValue = 25;
                ProgressText = "Lade AD-Attribute...";
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedItem.Abbreviation);
                ProgressValue = 100;
                ProgressText = "AD-Attribute geladen...";
                IsProgressBarVisible = false;
                ProgressText = "";
                return selectedItem;

            }
            set => SetProperty(ref selectedItem, value);
        }

        public ViewModelUserView(Repository repository)
        {
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => SelectedItem);
            Repository = repository;
            RecordsList = Repository.RecordsList;
        }



        private bool CanExecute()
        {
            return true; /*!String.IsNullOrEmpty(SelectedItem.Abbreviation);*/
        }

        private void Execute()
        {
            MessageBox.Show("Speichert...");
        }

        public void OpenWithDefaultProgram()
        {
            Process.Start(new ProcessStartInfo { FileName = "C:\\Temp\\PDF\\EintrittsPDF.pdf", UseShellExecute = true });
        }
    }
}
