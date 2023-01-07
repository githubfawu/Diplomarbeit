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
        public DelegateCommand CopyRightsCommand { get; set; }
        private Repository Repository { get; set; }

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

        private DirectReport selectedDirectReport;
        public DirectReport SelectedDirectReport
        {
            get => selectedDirectReport;
            set
            {
                if (value != selectedDirectReport)
                {
                    SetProperty(ref selectedDirectReport, value);
                }
            }
        }

        private Employee selectedAdGroup;
        public Employee SelectedAdGroup
        {
            get => selectedAdGroup;
            set
            {
                if (value != selectedAdGroup)
                {
                    SetProperty(ref selectedAdGroup, value);
                }
            }
        }

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

        private ObservableCollection<ActiveDirectoryGroup> adGroupList;
        public ObservableCollection<ActiveDirectoryGroup> AdGroupList
        {
            get => adGroupList;
            set
            {
                if (value != adGroupList)
                {
                    SetProperty(ref adGroupList, value);
                }
            }
        }

        private ObservableCollection<DirectReport> directReportList;
        public ObservableCollection<DirectReport> DirectReportList
        {
            get => directReportList;
            set
            {
                if (value != directReportList)
                {
                    SetProperty(ref directReportList, value);
                }
            }
        }

        private Record selectedRecord;
        public Record SelectedRecord
        {
            get
            {
                if (selectedRecord == null)
                {
                    return null;
                }
                IsBusy = true;
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedRecord.Abbreviation);
                AdGroupList = Repository.GetAllAdGroups(selectedRecord.Abbreviation);
                DirectReportList = Repository.GetAllDirectReports(CurrentEmployee.Manager);
                IsBusy = false;
                return selectedRecord;
            }
            set => SetProperty(ref selectedRecord, value);
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
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => SelectedRecord);
            GetNumberCommand = new DelegateCommand(ShowGetNumberWindow);
            OpenDocumentCommand = new DelegateCommand(OpenDocumentWithDefaultProgram);
            CopyRightsCommand = new DelegateCommand(CopyRights);
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
            var document = Repository.GetOriginalDocument(selectedRecord.AssociatedFile);
            if (document.Length > 0)
            {
                var temp = Path.GetTempPath() + "Eintritte.pdf";
                File.WriteAllBytes(temp, document);
                Process.Start(new ProcessStartInfo { FileName = temp, UseShellExecute = true });
            }
        }

        private void CopyRights()
        {
            Repository.CopyRightsFromUser(selectedDirectReport.SamAccountName, CurrentEmployee.Abbreviation);
            AdGroupList = Repository.GetAllAdGroups(selectedRecord.Abbreviation);
        }
        private void ShowGetNumberWindow()
        {
            if (CurrentEmployee != null)
            {
                GetNumberWindow window = new GetNumberWindow(CurrentEmployee, Repository);
                window.ShowDialog();
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedRecord.Abbreviation);
            }
            else
            {
                MessageBox.Show("Es muss zuerst ein Datensatz ausgewählt werden!");
            }
        }
    }
}
