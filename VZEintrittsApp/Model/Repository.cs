using System.Collections.Generic;
using System.Windows;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Import;
using VZEintrittsApp.Import.PDFReader;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private IReadDocument readDocument;
        private List<User> userList = new List<User>();

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

            MessageBox.Show(userList[0].Abbreviation.ToString());
        }
    }
}
