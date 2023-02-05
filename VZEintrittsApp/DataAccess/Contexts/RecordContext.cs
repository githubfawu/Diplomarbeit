using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VZEintrittsApp.Model.RecordEntity;

namespace VZEintrittsApp.DataAccess.Contexts
{
    public class RecordContext 
    {
        private DbContext DbContext;

        public RecordContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Record> GetAllClosedRecords()
        {
            var getRecords = DbContext.Records.Where(r => r.Status.RecordStatusId == 5).ToList();
            return getRecords;
        }
        public List<Record> GetAllOpenRecords()
        {
            var getRecords = DbContext.Records.Where(r => r.Status.RecordStatusId != 5).ToList();
            return getRecords;
        }
        public List<RecordStatus> GetAllRecordStatus()
        {
            var getStatus = DbContext.RecordStatus.ToList();
            return getStatus;
        }
        public bool RecordExists(Record record)
        {
            if(DbContext.Records.FirstOrDefault(r => r.EmployeeNr == record.EmployeeNr) != null) return true;
            return false;
        }

        public bool DeleteRecord(Record record)
        {
            if (DbContext.Records.FirstOrDefault(r => r.RecordId == record.RecordId) != null)
            {
                DbContext.Records.Remove(record);
                return true;
            }
            return false;
        }
        public byte[] GetEntryDocument(string filename)
        {
            SavedFile file = DbContext.SavedFiles.SingleOrDefault(x => x.FileName == filename);
            return file?.File;
        }
        public bool SaveNewRecord(Record record)
        {
            DbContext.Records.AddRange(record);
            DbContext.SaveChanges();
            return true;
        }
        public bool UpdateRecord(Record record)
        {
            if (record != null)
            {
                var recordOnDb = DbContext.Records.SingleOrDefault(x => x.RecordId == record.RecordId);
                if (recordOnDb != null)
                {
                    DbContext.Records.Update(recordOnDb);
                    DbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool SaveNewFile(SavedFile file)
        {
            DbContext.SavedFiles.AddRange(file);
            DbContext.SaveChanges();
            return true;
        }

        
    }
}
