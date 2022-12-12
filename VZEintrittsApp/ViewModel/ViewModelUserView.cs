using System;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        public DelegateCommand UpdateCommand { get; set; }
        public ViewModelUserView()
        {
            Abbreviation = "FWue";
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => Abbreviation);
        }


        private int employeeNr;
        public int EmployeeNr
        {
            get => employeeNr;
            set => SetProperty(ref employeeNr, value);
        }

        private string abbreviation;
        public string Abbreviation
        {
            get => abbreviation;
            set => SetProperty(ref abbreviation, value);
        }

        private bool CanExecute()
        {
            return !String.IsNullOrEmpty(Abbreviation);
        }

        private void Execute()
        {
            MessageBox.Show("Speichert...");
        }



    }
}
