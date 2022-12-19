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

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private RecordsHandler recordsHandler = new RecordsHandler();
        private DirectoryServices activeDirectory = new DirectoryServices();
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository()
        {
            ReadAllRecords();
        }

        public void ReadAllRecords()
        {
            RecordsList.AddRange(recordsHandler.GetAllRecords());
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return activeDirectory.GetAttributes(abbreviation);
        }

        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                documentReader = new ReadPdfDocument();
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
                recordFromDocument.Recorder = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                recordFromDocument.RecordRead = DateTime.Now;

                recordsHandler.SaveNewRecord(recordFromDocument);
                RecordsList.Add(recordFromDocument);
            }

            var fileToSave = new SavedFile()
            {
                FileName = fileName,
                File = File.ReadAllBytes(file)
            };
            
            recordsHandler.SaveNewFile(fileToSave);

            activeDirectory.CreateNewAdAccount(documentReader.ReadUsers(file));

        }
    }
}
