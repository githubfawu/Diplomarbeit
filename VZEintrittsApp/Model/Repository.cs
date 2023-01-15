using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.API.AD;
using VZEintrittsApp.Import;
using VZEintrittsApp.Import.PDFReader;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private RecordContext RecordContext;
        private FinalizeContext FinalizeContext;
        private LoggerContext Log;
        private PhoneFormatContext PhoneFormat;
        private ManagementLevelContext ManagementLevelContext;
        private DirectoryServices activeDirectory;
        private ReadPdfDocument ReadPdfDocument;
        private FinalizeEmployee FinalizeEmployee;
        private AddIndividualProperties AddIndividualProperties;
        private List<ManagementLevel> ManagementLevelList;
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository(
            RecordContext recordContext,
            PhoneFormatContext phoneFormatContext,
            LoggerContext log,
            FinalizeContext finalizeContext,
            ManagementLevelContext managementLevelContext,
            DirectoryServices directoryServices,
            ReadPdfDocument readPdfDocument,
            FinalizeEmployee finalizeEmployee,
            AddIndividualProperties addIndividualProperties)
        {
            activeDirectory = directoryServices;
            FinalizeContext = finalizeContext;
            RecordContext = recordContext;
            ManagementLevelContext = managementLevelContext;
            PhoneFormat = phoneFormatContext;
            ReadPdfDocument = readPdfDocument;
            FinalizeEmployee = finalizeEmployee;
            AddIndividualProperties = addIndividualProperties;
            ManagementLevelList = ManagementLevelContext.GetAllManagementLevels().ToList();
            Log = log;
            GetAllOpenRecords();
        }

        public void GetAllClosedRecords()
        {
            RecordsList.Clear();
            RecordsList.AddRange(RecordContext.GetAllClosedRecords());
        }

        public void GetAllOpenRecords()
        {
            RecordsList.Clear();
            RecordsList.AddRange(RecordContext.GetAllOpenRecords());
        }
        public bool UpdateRecord(Record record, Employee employee)
        {
            if (RecordContext.UpdateRecord(record))
            {
                WriteSpecificAdAttribute("VzStartDate", $"{record.FirstWorkingDay!:dd.MM.yyyy}", employee);
                return true;
            }
            return false;
        }

        public ObservableCollection<Note> GetAllNotes(string description)
        {
            return null;
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return activeDirectory.GetAttributes(abbreviation, ManagementLevelContext.GetAllManagementLevels());
        }

        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                documentReader = ReadPdfDocument;
            }
            else
            {
                MessageBox.Show("Kein gültiges Dateiformat erkannt!");
                return;
            }

            var fileName = ($"{DateTime.Now.Ticks}{Path.GetFileName(file)}");

            foreach (var recordFromDocument in documentReader.ReadRecords(file))
            {
                recordFromDocument.Status = RecordContext.GetAllRecordStatus().SingleOrDefault(x => x.RecordStatusId == 1);
                recordFromDocument.AssociatedFile = fileName;
                recordFromDocument.Recorder = WindowsIdentity.GetCurrent().Name;
                recordFromDocument.RecordReadDate = DateTime.Now;

                if (RecordContext.GetRecord(recordFromDocument) == false)
                {
                    RecordContext.SaveNewRecord(recordFromDocument);
                    Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, recordFromDocument.Abbreviation, "Ein neuer Eintrittsdatensatz wurde erstellt.");
                    RecordsList.Add(recordFromDocument);
                }
                else
                {
                    MessageBox.Show($"Für den Benutzer {recordFromDocument.Abbreviation} existiert bereits ein offener Datensatz!");
                }
            }

            var fileToSave = new SavedFile()
            {
                FileName = fileName,
                File = File.ReadAllBytes(file)
            };
            
            RecordContext.SaveNewFile(fileToSave);
            ReadAndCreateEmployees(file);
        }

        private void ReadAndCreateEmployees(string file)
        {
            var users = documentReader.ReadUsers(file);
            foreach (var user in users)
            {
                if (activeDirectory.CheckIfUserExists(user.Abbreviation) == false)
                {
                    FinalizeEmployee.FinalizeEmployees(user);
                    AddIndividualProperties.AddProperties(user, FinalizeContext.GetAppropriateSubsidiaryCompany(user.City, user.Company));
                    activeDirectory.CreateNewAdAccount(user);
                    activeDirectory.AddManagementGroupToUser(user.Abbreviation, GetCorrespondingManagementLevel(user.VzManagementLevel.MgmtLevel));
                }
                else
                {
                    MessageBox.Show($"Der Benutzer {user.Abbreviation} existiert bereits im Active Directory!");
                }
            }
        }

        private ManagementLevel GetCorrespondingManagementLevel(string managmentLevel)
        {
            if (string.IsNullOrWhiteSpace(managmentLevel))
            {
                return ManagementLevelContext.GetSingleManagementLevel("Keine");
            }
            return ManagementLevelContext.GetSingleManagementLevel(managmentLevel);
        }

        public ObservableCollection<ActiveDirectoryGroup> GetAllAdGroups(string abbreviation)
        {
            var observableGroup = new ObservableCollection<ActiveDirectoryGroup>();
            foreach (var group in activeDirectory.GetAdGroupsFromUser(abbreviation))
            {
                observableGroup.Add(group);
            }
            return observableGroup;
        }

        public ObservableCollection<RecordStatus> GetAllARecordStatus()
        {
            var observableGroup = new ObservableCollection<RecordStatus>();
            foreach (var status in RecordContext.GetAllRecordStatus())
            {
                observableGroup.Add(status);
            }
            return observableGroup;
        }

        public ObservableCollection<DirectReport> GetAllDirectReports(string managersAbbreviation)
        {
            var observableGroup = new ObservableCollection<DirectReport>();
            foreach (var directReport in activeDirectory.GetDirectReports(managersAbbreviation))
            {
                observableGroup.Add(directReport);
            }
            return observableGroup;
        }

        public ObservableCollection<ManagementLevel> GetAllManagementLevels()
        {
            var observableGroup = new ObservableCollection<ManagementLevel>();
            foreach (var managementLevel in ManagementLevelList)
            {
                observableGroup.Add(managementLevel);
            }
            return observableGroup;
        }

        public bool CopyRightsFromUser(string sourceAbbreviation, string targetAbbreviation)
        {
            if (!string.IsNullOrWhiteSpace(sourceAbbreviation) && !string.IsNullOrWhiteSpace(targetAbbreviation))
            {
                if (activeDirectory.CopyRightsFromOtherUser(sourceAbbreviation, targetAbbreviation)) return true;
            }
            MessageBox.Show("Nicht alle benötigten Daten (Kürzel von User oder dem ausgewählten unterstellten) sind vorhanden");
            return false;
        }

        public bool RemoveGroupFromUser(string abbreviation, string groupName)
        {
            if (!string.IsNullOrWhiteSpace(abbreviation) && !string.IsNullOrWhiteSpace(groupName))
            {
                if (activeDirectory.RemoveGroupFromUser(abbreviation, groupName)) return true;
            }
            MessageBox.Show($"Die AD-Gruppe {groupName} konnte nicht entfernt werden.");
            return false;
        }

        public bool WriteSpecificAdAttribute(string employeeAttributeName, string value, Employee employee)
        {
            if (activeDirectory.WriteIndividualAttribute(employeeAttributeName, value, ManagementLevelList, employee)) return true;
            return false;
        }

        public string[] GetFreeNumberFromAd(string description)
        {
            var subsidiaryCompany = FinalizeContext.GetSubsidiaryCompanyFromDescription(description);
            var lowRange = subsidiaryCompany.PhoneNumberRangeLow;
            var highRange = subsidiaryCompany.PhoneNumberRangeHigh;

            string[] numbers = new string[3];
            for (long i = lowRange; i < highRange; i++)
            {
                if (activeDirectory.IsNumberFreeChecker(i))
                {
                    MessageBoxResult result = MessageBox.Show($"Ist die Nummer +{i} verfügbar?", "Bitte Testanruf durchführen...", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        numbers[0] = $"+{i}";
                        if (GetCorrectNumberFormat(numbers[0], subsidiaryCompany.CityName) == null)
                        {
                            MessageBox.Show("Es konnte keine Nummer-Formatierung für diesen Standort gefunden werden. Bitte formatiere die Nummer manuell.");
                            numbers[1] = $"+{i}";
                            break;
                        }
                        numbers[1] = GetCorrectNumberFormat(numbers[0], subsidiaryCompany.CityName);

                        if (subsidiaryCompany.IsOutgoingNumberAnonymous == true)
                        {
                            numbers[2] = "ANONYMOUS";
                        }
                        else
                        {
                            numbers[2] = subsidiaryCompany.OfficialPhoneNumber;
                        }
                        
                        break;
                    }
                    if (result == MessageBoxResult.Cancel)
                    {
                        return null;
                    }
                }
            }

            if (numbers[0] == null)
            {
                MessageBox.Show(
                    "Es konnte keine freie Nummer in dieser Range gefunden werden. Bitte wähle eine andere Range aus.");
                return null;
            }
            return numbers;
        }

        public string GetCorrectNumberFormat(string number, string cityName)
        {
            StringBuilder currentNumber = new StringBuilder();
            currentNumber.Append(number);

            var numberFormat = PhoneFormat.GetPhoneNumberFormat(cityName);
            foreach (var format in numberFormat.Formats)
            {
                currentNumber.Insert(format.Key + 1, format.Value);
            }
            return currentNumber.ToString();
        }

        public byte[] GetOriginalDocument(string filename)
        {
            var document = RecordContext.GetEntryDocument(filename);
            return document;
        }

        public ObservableCollection<SubsidiaryCompany> GetAllSubsidiaries()
        {
            ObservableCollection<SubsidiaryCompany> observableList = new ObservableCollection<SubsidiaryCompany>();
            var list = FinalizeContext.GetAllSubsidiaryCompanies().ToList();
            var listSorted =  list.OrderBy(s => s.CityName);
            foreach (var company in listSorted)
            {
                observableList.Add(company);
            }
            return observableList;
        }

    }
}
