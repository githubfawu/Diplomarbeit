using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class ContextHelper 
    {
        private DbContext dbContext = new DbContext();
        public ContextHelper() { }

        public List<Record> GetAllRecords()
        {
            var getRecords = dbContext.Records.ToList();
            return getRecords;
        }
        public bool GetRecord(Record record)
        {
            if(dbContext.Records.FirstOrDefault(r => r.EmployeeNr == record.EmployeeNr) != null) return true;
            return false;
        }
        public bool SaveNewRecord(Record record)
        {
            dbContext.Records.AddRange(record);
            dbContext.SaveChanges();
            return true;
        }

        public bool SaveNewFile(SavedFile file)
        {
            dbContext.SavedFiles.AddRange(file);
            dbContext.SaveChanges();
            return true;
        }

        public byte[] GetEntryDocument(string filename)
        {
            SavedFile file = dbContext.SavedFiles.SingleOrDefault(x => x.FileName == filename);
            return file?.File;
        }

        public StateAndCountry? GetStateAndCountry (string cityName)
        {
            if (dbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName) != null)
            {
                return dbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName);
            }
            else
            {
                MessageBox.Show("Fehler beim Laden des Kantons und des Landes!");
                return null;
            }
        }
    }
}
