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
using System.Security.Principal;
using System.Text;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private RecordContext RecordContext;
        private FinalizeContext FinalizeContext;
        private LoggerContext Log;
        private DirectoryServices activeDirectory;
        private ReadPdfDocument ReadPdfDocument;
        private FinalizeEmployee FinalizeEmployee;
        private AddIndividualProperties AddIndividualProperties;
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository(
            RecordContext recordContext,
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
                recordFromDocument.RecordRead = DateTime.Now;

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
                        numbers[1] = GetCorrectNumberFormat(numbers[0], description);
                        break;
                    }
                    if (result == MessageBoxResult.Cancel)
                    {
                        return null;
                    }
                }
            }

            if (numbers[0] == "")
            {
                MessageBox.Show(
                    "Es konnte keine freie Nummer in dieser Range gefunden werden. Bitte wähle eine andere Range aus.");
                return null;
            }
            else
            {
                return numbers;
            }
        }

        public string GetCorrectNumberFormat(string number, string description)
        {
            StringBuilder currentNumber = new StringBuilder();
            currentNumber.Append(number);

            List<Tuple<int, string>> positionList = new List<Tuple<int, string>>();
            positionList.Add(new Tuple<int, string>(2, " "));
            positionList.Add(new Tuple<int, string>(5, " "));
            positionList.Add(new Tuple<int, string>(9, " "));
            positionList.Add(new Tuple<int, string>(12, " "));

            //zu tun: Description zu City umwandeln, neue Tabelle mit City und Format erstellen, reader im Context erstellen

            foreach (var position in positionList)
            {
                currentNumber.Insert(position.Item1 + 1, position.Item2);
            }
            
            return currentNumber.ToString();
        }
        public byte[] GetOriginalDocument(string filename)
        {
            var document = RecordContext.GetEntryDocument(filename);
            return document;
        }

    }
}
