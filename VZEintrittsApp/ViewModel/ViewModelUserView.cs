using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using KellermanSoftware.CompareNetObjects;
using Prism.Commands;
using Prism.Mvvm;
using VZEintrittsApp.Model;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.RecordEntity;
using VZEintrittsApp.View;
using Employee = VZEintrittsApp.Model.Employee.Employee;

namespace VZEintrittsApp.ViewModel
{
    class ViewModelUserView : BindableBase
    {
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand SaveRecordCommand { get; set; }
        public DelegateCommand ShowClosedRecordsCommand { get; set; }
        public DelegateCommand ShowOpenRecordsCommand { get; set; }
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

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set
            {
                if (value != currentEmployeeBeforeChanges)
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

        private ObservableCollection<Note> allNotes;
        public ObservableCollection<Note> AllNotes
        {
            get => allNotes;
            set
            {
                if (value != allNotes)
                {
                    SetProperty(ref allNotes, value);
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
                LoadEmployeeFromAd();
                CloneEmployeeForDifferenceCheck();
                DirectReportList = Repository.GetAllDirectReports(CurrentEmployee.Manager, CurrentEmployee.Abbreviation);
                AllNotes = Repository.GetAllNotes(CurrentEmployee.Description);
                IsBusy = false;
                return selectedRecord;
            }
            set
            {
                if (CurrentEmployee != null && CurrentEmployeeBeforeChanges != null)
                {
                    ComparisonResult compareResult = Compare.Compare(CurrentEmployee, CurrentEmployeeBeforeChanges);
                    if (!compareResult.AreEqual)
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "Der vorherige Benutzer hat Änderungen, welche noch nicht gespeichert wurden. Möchtest du die Änderungen speichern?",
                            "Hinweis", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            SaveChangesOnEmployee(true);
                            SetProperty(ref selectedRecord, value);
                        }
                        else
                        {
                            if (value != selectedRecord)
                            {
                                SetProperty(ref selectedRecord, value);
                            }
                        }
                    }
                    else
                    {
                        if (value != selectedRecord)
                        {
                            SetProperty(ref selectedRecord, value);
                        }
                    }
                }
                if (value != selectedRecord)
                {
                    SetProperty(ref selectedRecord, value);
                }
            }
        }



        public ViewModelUserView(Repository repository)
        {
            Repository = repository;
            RecordsList = Repository.RecordsList;
            RecordStatusList = Repository.GetAllARecordStatus();
            MgmtLevels = Repository.GetAllManagementLevels();
            AllNotes = new ObservableCollection<Note>();

            SaveCommand = new DelegateCommand(ExecuteSave);
            SaveRecordCommand = new DelegateCommand(SaveChangesOnRecord);
            ShowClosedRecordsCommand = new DelegateCommand(GetAllClosedRecords);
            ShowOpenRecordsCommand = new DelegateCommand(GetAllOpenRecords);
            GetNumberCommand = new DelegateCommand(ShowGetNumberWindow);
            OpenDocumentCommand = new DelegateCommand(OpenDocumentWithDefaultProgram);
            CopyRightsCommand = new DelegateCommand(CopyRights);
            RemoveGroupCommand = new DelegateCommand(RemoveGroup);
            
            Compare = new CompareLogic();
            Compare.Config.MaxDifferences = 200;

            timer = new System.Timers.Timer(1800);
            timer.AutoReset = false;
            timer.Elapsed += TimerTicked;
            IsBusy = false;
        }

        public void ExecuteSave()
        {
            SaveChangesOnEmployee(true);
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
        private void GetAllClosedRecords()
        {
            Repository.GetAllClosedRecords();
        }

        private void GetAllOpenRecords()
        {
            Repository.GetAllOpenRecords();
        }

        private bool SaveChangesOnEmployee(bool showLabel)
        {
            isBusy = true;
            ComparisonResult result = Compare.Compare(CurrentEmployee, CurrentEmployeeBeforeChanges);
            if (!result.AreEqual)
            {
                foreach (var difference in result.Differences)
                {
                    Repository.WriteSpecificAdAttribute(difference.PropertyName, difference.Object1Value, CurrentEmployee);
                }

                if (showLabel == true)
                {
                    ChangeStatusSavedLabel();
                }
                CloneEmployeeForDifferenceCheck();
                isBusy = false;
            }
            LoadEmployeeFromAd();
            return true;
        }

        private void SaveChangesOnRecord()
        {
            isBusy = true;
            if (Repository.UpdateRecord(selectedRecord, CurrentEmployee)) ChangeStatusSavedLabel();
            LoadEmployeeFromAd();
            CloneEmployeeForDifferenceCheck();
            isBusy = false;
        }

        private void LoadEmployeeFromAd()
        {
            CurrentEmployee = Repository.ReadAllAdAttributes(selectedRecord.Abbreviation);
            AdGroupList = Repository.GetAllAdGroups(selectedRecord.Abbreviation);
        }

        private void CloneEmployeeForDifferenceCheck()
        {
            CurrentEmployeeBeforeChanges = CurrentEmployee.Clone() as Employee;
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
            SaveChangesOnEmployee(false);
            isBusy = false;
        }

        private void CopyRights()
        {
            isBusy = true;
            Repository.CopyRightsFromUser(selectedDirectReport.SamAccountName, CurrentEmployee.Abbreviation);
            SaveChangesOnEmployee(false);
            isBusy = false;
        }
        private void ShowGetNumberWindow()
        {
            if (CurrentEmployee != null)
            {
                GetNumberWindow window = new GetNumberWindow(CurrentEmployee, Repository);
                window.ShowDialog();
                LoadEmployeeFromAd();
            }
            else
            {
                MessageBox.Show("Es muss zuerst ein Datensatz ausgewählt werden!");
            }
        }
    }
}
