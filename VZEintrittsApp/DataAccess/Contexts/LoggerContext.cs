using System;
using VZEintrittsApp.Logger;

namespace VZEintrittsApp.DataAccess.Contexts
{
    public class LoggerContext
    {
        private DbContext DbContext;
        public LoggerContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Write(DateTime timestamp, string user, string targetAbbreciation, string recordEvent)
        {
            var log = new Log()
            {
                Timestamp = timestamp,
                Originator = user,
                SelectedEmployee = targetAbbreciation,
                RecordEvent = recordEvent
            };

            DbContext.Logs.Add(log);
            DbContext.SaveChanges();
        }
    }
}