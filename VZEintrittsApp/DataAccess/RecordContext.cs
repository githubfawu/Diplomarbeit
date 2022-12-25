using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class RecordContext 
    {
        private DbContext DbContext;

        public RecordContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Record> GetAllRecords()
        {
            var getRecords = DbContext.Records.ToList();
            return getRecords;
        }
        public bool GetRecord(Record record)
        {
            if(DbContext.Records.FirstOrDefault(r => r.EmployeeNr == record.EmployeeNr) != null) return true;
            return false;
        }
        public bool SaveNewRecord(Record record)
        {
            DbContext.Records.AddRange(record);
            DbContext.SaveChanges();
            return true;
        }

        public bool SaveNewFile(SavedFile file)
        {
            DbContext.SavedFiles.AddRange(file);
            DbContext.SaveChanges();
            return true;
        }

        public byte[] GetEntryDocument(string filename)
        {
            SavedFile file = DbContext.SavedFiles.SingleOrDefault(x => x.FileName == filename);
            return file?.File;
        }

        public StateAndCountry? GetStateAndCountry (string cityName)
        {
            if (DbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName) != null)
            {
                return DbContext.StatesAndCountries.SingleOrDefault(x => x.CityName == cityName);
            }
            else
            {
                MessageBox.Show("Fehler beim Laden des Kantons und des Landes!");
                return null;
            }
        }
    }
}
