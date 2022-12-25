using System;
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

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private RecordContext RecordContext;
        private LoggerContext Log;
        private DirectoryServices activeDirectory;
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository(RecordContext recordContext, LoggerContext log, DirectoryServices directoryServices)
        {
            activeDirectory = directoryServices;
            RecordContext = recordContext;
            ReadAllRecords();
            Log = log;
        }

        public void ReadAllRecords()
        {
            RecordsList.AddRange(RecordContext.GetAllRecords());
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return activeDirectory.GetAttributes(abbreviation);
        }

        public StateAndCountry? GetStateAndCountry(string cityName)
        {
            return RecordContext.GetStateAndCountry(cityName);
        }

        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                documentReader = new ReadPdfDocument(RecordContext);
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
                }
                else
                {
                    MessageBox.Show($"Für den Benutzer {recordFromDocument.Abbreviation} existiert bereits ein Eintrag in der Datenbank!");
                }
                
                RecordsList.Add(recordFromDocument);
            }

            var fileToSave = new SavedFile()
            {
                FileName = fileName,
                File = File.ReadAllBytes(file)
            };
            
            RecordContext.SaveNewFile(fileToSave);

            var users = documentReader.ReadUsers(file);
            foreach (var user in users)
            {
                if (activeDirectory.CheckIfUserExists(user.Abbreviation) == false)
                {
                    activeDirectory.CreateNewAdAccount(user);
                }
                else
                {
                    MessageBox.Show($"Der Benutzer {user.Abbreviation} existiert bereits im AD!");
                }
            }
        }

        public byte[] GetOriginalDocument(string filename)
        {
            var document = RecordContext.GetEntryDocument(filename);
            return document;
        }
    }
}
