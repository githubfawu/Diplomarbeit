using System.Collections.Generic;
using System.Linq;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class RecordsHandler 
    {
        private DbContext dbContext = new DbContext();
        public RecordsHandler() { }

        public List<Record> GetAllRecords()
        {
            var getRecords = dbContext.Records.ToList();
            return getRecords;
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



    }
}
