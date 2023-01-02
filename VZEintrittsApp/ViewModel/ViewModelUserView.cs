using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using VZEintrittsApp.View;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand GetNumberCommand { get; set; }
        public DelegateCommand OpenDocumentCommand { get; set; }

        private Repository Repository { get; set; }

        private ObservableCollection<Record> recordsList;
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
                IsBusy = true;
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedItem.Abbreviation);
                IsBusy = false;
                return selectedItem;
            }
            set => SetProperty(ref selectedItem, value);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
            }
        }

        public ViewModelUserView(Repository repository)
        {
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => SelectedItem);
            GetNumberCommand = new DelegateCommand(ShowGetNumberWindow);
            OpenDocumentCommand = new DelegateCommand(OpenDocumentWithDefaultProgram);
            Repository = repository;
            RecordsList = Repository.RecordsList;
        }

        private bool CanExecute()
        {
            return true; /*!string.IsNullOrEmpty(SelectedItem.Abbreviation);*/
        }

        private void Execute()
        {
            MessageBox.Show("Speichert...");
        }

        public void OpenDocumentWithDefaultProgram()
        {
            var document = Repository.GetOriginalDocument(selectedItem.AssociatedFile);
            if (document.Length > 0)
            {
                var temp = Path.GetTempPath() + "Eintritte.pdf";
                File.WriteAllBytes(temp, document);
                Process.Start(new ProcessStartInfo { FileName = temp, UseShellExecute = true });
            }
        }
        private void ShowGetNumberWindow()
        {
            if (CurrentEmployee != null)
            {
                GetNumberWindow window = new GetNumberWindow(CurrentEmployee, Repository);
                window.ShowDialog();
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedItem.Abbreviation);
            }
            else
            {
                MessageBox.Show("Es muss zuerst ein Datensatz ausgewählt werden!");
            }
        }
    }
}
