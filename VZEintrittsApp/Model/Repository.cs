﻿using System.Collections.Generic;
using System.Windows;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Import;
using VZEintrittsApp.Import.PDFReader;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private IReadDocument readDocument;
        private List<Employee> userList = new List<Employee>();

        public Repository()
        {
        }

        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                readDocument = new ReadPdfDocument();
            }
            else
            {
                MessageBox.Show("Das Datei-Format konnte nicht erkannt werden!");
                return;
            }
            
            userList.AddRange(readDocument.ProcessDocument(file));

            SaveDocument(System.IO.File.ReadAllBytes(file));


            MessageBox.Show(userList[0].Abbreviation.ToString());
        }

        private void SaveDocument(byte[] file)
        {

        }
    }
}
