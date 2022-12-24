﻿using System;
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
using System.IO.Pipes;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private ContextHelper contextHelper = new ContextHelper();
        private DirectoryServices activeDirectory = new DirectoryServices();
        private IReadDocument documentReader;
        public ObservableCollection<Record> RecordsList = new ObservableCollection<Record>();

        public Repository()
        {
            ReadAllRecords();
        }

        public void ReadAllRecords()
        {
            RecordsList.AddRange(contextHelper.GetAllRecords());
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return activeDirectory.GetAttributes(abbreviation);
        }

        public StateAndCountry? GetStateAndCountry(string cityName)
        {
            return contextHelper.GetStateAndCountry(cityName);
        }
        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                documentReader = new ReadPdfDocument(contextHelper);
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

                if (contextHelper.GetRecord(recordFromDocument) == false)
                {
                    contextHelper.SaveNewRecord(recordFromDocument);
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
            
            contextHelper.SaveNewFile(fileToSave);

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
            var document = contextHelper.GetEntryDocument(filename);
            return document;
        }
    }
}
