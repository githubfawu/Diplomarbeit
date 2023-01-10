using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess.Seeders
{
    public static class ManagementLevelSeeder
    {
        public static List<ManagementLevel> GetSeeds()
        {
            var list = new List<ManagementLevel>()
            {
                new ManagementLevel(){MgmtLevelId = 1, MgmtLevel = "Kaderstufe 1", MgmtLevelGroupName = "USR-G-ORG-VZ_Kaderstufe_1"},
                new ManagementLevel(){MgmtLevelId = 2, MgmtLevel = "Kaderstufe 2", MgmtLevelGroupName = "USR-G-ORG-VZ_Kaderstufe_2"},
                new ManagementLevel(){MgmtLevelId = 3, MgmtLevel = "Kaderstufe 3", MgmtLevelGroupName = "USR-G-ORG-VZ_Kaderstufe_3"},
                new ManagementLevel(){MgmtLevelId = 4, MgmtLevel = "Kaderstufe 4", MgmtLevelGroupName = "USR-G-ORG-VZ_Kaderstufe_4"},
                new ManagementLevel(){MgmtLevelId = 5, MgmtLevel = "Keine", MgmtLevelGroupName = ""}
            };
            return list;
        }
    }
}
