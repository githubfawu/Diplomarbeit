using System.Collections.Generic;
using System.Windows;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.API.AD;
using VZEintrittsApp.Import;
using VZEintrittsApp.Import.PDFReader;

namespace VZEintrittsApp.Model
{
    public class Repository
    {
        private RecordsHelper recordsHandler = new RecordsHelper();
        private DirectoryServices activeDirectory = new DirectoryServices();
        private IReadDocument documentReader;
        private List<Employee> userList = new List<Employee>();
        public List<Record> RecordsList = new List<Record>();

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

        public Employee GetAdObject(string name)
        {
            if (name == "FWue")
            {
                return new Employee()
                {
                    Abbreviation = "FWue",
                    EmployeeNr = 11
                };
            }
            else
            {
                return new Employee()
                {
                    Abbreviation = "GBam",
                    EmployeeNr = 11
                };
            }
            
        }

        public void ImportDocument(string file)
        {
            if(file.Contains(".pdf"))
            {
                documentReader = new ReadPdfDocument();
            }
            else
            {
                MessageBox.Show("Das Datei-Format konnte nicht erkannt werden!");
                return;
            }
            
            userList.AddRange(documentReader.ProcessDocument(file));

            SaveDocument(System.IO.File.ReadAllBytes(file));


            MessageBox.Show(userList[0].Abbreviation.ToString());
        }



        private void SaveDocument(byte[] file)
        {

        }
    }
}
