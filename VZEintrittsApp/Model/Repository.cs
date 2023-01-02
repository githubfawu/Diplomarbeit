using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.API.AD;
using VZEintrittsApp.Enums;
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
        private DirectoryServices activeDirectory;
        private ReadPdfDocument ReadPdfDocument;
        private FinalizeEmployee FinalizeEmployee;
        private AddIndividualProperties AddIndividualProperties;
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository(
            RecordContext recordContext,
            PhoneFormatContext phoneFormatContext,
            LoggerContext log,
            FinalizeContext finalizeContext,
            DirectoryServices directoryServices,
            ReadPdfDocument readPdfDocument,
            FinalizeEmployee finalizeEmployee,
            AddIndividualProperties addIndividualProperties)
        {
            activeDirectory = directoryServices;
            FinalizeContext = finalizeContext;
            RecordContext = recordContext;
            PhoneFormat = phoneFormatContext;
            ReadPdfDocument = readPdfDocument;
            FinalizeEmployee = finalizeEmployee;
            AddIndividualProperties = addIndividualProperties;
            Log = log;
            ReadAllRecords();
        }

        public void ReadAllRecords()
        {
            RecordsList.AddRange(RecordContext.GetAllRecords());
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return activeDirectory.GetAttributes(abbreviation);
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
                recordFromDocument.Status = RecordStatus.Offen;
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
                }
                else
                {
                    MessageBox.Show($"Der Benutzer {user.Abbreviation} existiert bereits im Active Directory!");
                }
            }
        }

        public bool WriteSpecificAdAttribute(string employeeAttributeName, string abbreviation, string value)
        {
            if (activeDirectory.WriteIndividualAttribute(employeeAttributeName, abbreviation, value)) return true;
            return false;
        }

        public string[] GetFreeNumberFromAd(string description)
        {
            var subsidiaryCompany = FinalizeContext.GetSubsidiaryCompanyFromDescription(description);
            var lowRange = subsidiaryCompany.PhoneNumberRangeLow;
            var highRange = subsidiaryCompany.PhoneNumberRangeHigh;

            string[] numbers = new string[2];
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
                            MessageBox.Show("Es konnte keine Nummern-Formatierung für diesen Standort gefunden werden. Bitte formatiere die Nummer manuell.");
                            numbers[1] = $"+{i}";
                            break;
                        }
                        numbers[1] = GetCorrectNumberFormat(numbers[0], subsidiaryCompany.CityName);
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
