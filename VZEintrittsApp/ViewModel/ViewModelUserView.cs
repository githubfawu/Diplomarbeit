using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using KellermanSoftware.CompareNetObjects;
using Prism.Commands;
using Prism.Mvvm;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;
using VZEintrittsApp.View;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand SaveRecordCommand { get; set; }
        public DelegateCommand GetNumberCommand { get; set; }
        public DelegateCommand OpenDocumentCommand { get; set; }
        public DelegateCommand CopyRightsCommand { get; set; }
        public DelegateCommand RemoveGroupCommand { get; set; }
        private Repository Repository { get; set; }
        private CompareLogic Compare { get; set; }

        private System.Timers.Timer timer;
        private bool showLabelSaved;
        public bool ShowLabelSaved
        {
            get => showLabelSaved;
            set
            {
                if (value != showLabelSaved)
                {
                    SetProperty(ref showLabelSaved, value);
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

        private Employee currentEmployeeBeforeChanges;
        public Employee CurrentEmployeeBeforeChanges
        {
            get => currentEmployeeBeforeChanges;
            set
            {
                if (value != currentEmployeeBeforeChanges)
                {
                    SetProperty(ref currentEmployeeBeforeChanges, value);
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

        private ActiveDirectoryGroup selectedAdGroup;
        public ActiveDirectoryGroup SelectedAdGroup
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

        private ObservableCollection<ManagementLevel> mgmtLevels;
        public ObservableCollection<ManagementLevel> MgmtLevels
        {
            get => mgmtLevels;
            set
            {
                if (value != mgmtLevels)
                {
                    SetProperty(ref mgmtLevels, value);
                }
            }
        }

        private ObservableCollection<RecordStatus> recordStatusList;
        public ObservableCollection<RecordStatus> RecordStatusList
        {
            get => recordStatusList;
            set
            {
                if (value != recordStatusList)
                {
                    SetProperty(ref recordStatusList, value);
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

                CompareEmployeeObjects(CurrentEmployee, CurrentEmployeeBeforeChanges);
                IsBusy = true;
                CurrentEmployee = Repository.ReadAllAdAttributes(selectedRecord.Abbreviation);
                CurrentEmployeeBeforeChanges = CurrentEmployee.Clone() as Employee;
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
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public ViewModelUserView(Repository repository)
        {
            Repository = repository;
            RecordsList = Repository.RecordsList;
            RecordStatusList = Repository.GetAllARecordStatus();
            MgmtLevels = Repository.GetAllManagementLevels();

            SaveCommand = new DelegateCommand(SaveChangesOnEmployee);
            SaveRecordCommand = new DelegateCommand(SaveChangesOnRecord);
            GetNumberCommand = new DelegateCommand(ShowGetNumberWindow);
            OpenDocumentCommand = new DelegateCommand(OpenDocumentWithDefaultProgram);
            CopyRightsCommand = new DelegateCommand(CopyRights);
            RemoveGroupCommand = new DelegateCommand(RemoveGroup);
            
            Compare = new CompareLogic();
            Compare.Config.MaxDifferences = 200;

            timer = new System.Timers.Timer(1800);
            timer.AutoReset = false;
            timer.Elapsed += TimerTicked;
            ShowLabelSaved = false;
        }

        private void ChangeStatusSavedLabel()
        {
            timer.Enabled = true;
            ShowLabelSaved = true;
        }

        private void TimerTicked(object sender, EventArgs e)
        {
            ShowLabelSaved = false;
        }

        private bool CompareEmployeeObjects(object currentUser, object currentUserBeforeChange)
        {
            ComparisonResult result = Compare.Compare(currentUser, currentUserBeforeChange);
            return result.AreEqual;
        }

        private void SaveChangesOnEmployee()
        {
            isBusy = true;
            ComparisonResult result = Compare.Compare(CurrentEmployee, CurrentEmployeeBeforeChanges);
            if (result.AreEqual) return;
            foreach (var difference in result.Differences)
            {
                Repository.WriteSpecificAdAttribute(difference.PropertyName, CurrentEmployee.Abbreviation, difference.Object1Value);
            }
            ChangeStatusSavedLabel();
            CurrentEmployeeBeforeChanges = CurrentEmployee.Clone() as Employee;
            RefreshEmployee();
            isBusy = false;
        }

        private void SaveChangesOnRecord()
        {
            isBusy = true;
            if (Repository.UpdateRecord(selectedRecord)) ChangeStatusSavedLabel();
            isBusy = false;
        }

        private void RefreshEmployee()
        {
            CurrentEmployee = Repository.ReadAllAdAttributes(selectedRecord.Abbreviation);
            AdGroupList = Repository.GetAllAdGroups(selectedRecord.Abbreviation);
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
        private void RemoveGroup()
        {
            isBusy = true;
            Repository.RemoveGroupFromUser(CurrentEmployee.Abbreviation, selectedAdGroup.AdGroupName);
            RefreshEmployee();
            isBusy = false;
        }

        private void CopyRights()
        {
            isBusy = true;
            Repository.CopyRightsFromUser(selectedDirectReport.SamAccountName, CurrentEmployee.Abbreviation);
            RefreshEmployee();
            isBusy = false;
        }
        private void ShowGetNumberWindow()
        {
            if (CurrentEmployee != null)
            {
                GetNumberWindow window = new GetNumberWindow(CurrentEmployee, Repository);
                window.ShowDialog();
                RefreshEmployee();
            }
            else
            {
                MessageBox.Show("Es muss zuerst ein Datensatz ausgewählt werden!");
            }
        }
    }
}
