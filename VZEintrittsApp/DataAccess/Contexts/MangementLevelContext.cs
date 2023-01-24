using System.Collections.Generic;
using System.Linq;
using VZEintrittsApp.Model.Domain;

namespace VZEintrittsApp.DataAccess.Contexts
{
    public class ManagementLevelContext
    {
        private DbContext DbContext;
        public ManagementLevelContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<ManagementLevel> GetAllManagementLevels()
        {
            var allManagementLevels = (from a in DbContext.ManagementLevels select a).ToList();
            return allManagementLevels;
        }

        public ManagementLevel GetSingleManagementLevel(string managementLevel)
        {
            return DbContext.ManagementLevels.SingleOrDefault(x => x.MgmtLevel == managementLevel);
        }
    }
}
