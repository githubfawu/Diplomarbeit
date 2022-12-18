using System.Collections.Generic;
using System.Linq;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess
{
    public class RecordsHelper
    {
        private List<Record> records = new List<Record>();
        private DBContext dbContext = new DBContext();
        public RecordsHelper() { }

        public List<Record> GetAllRecords()
        {
            var getRecords = dbContext.Records.ToList();
            return getRecords;
        }

    }
}
