using System.Collections.Generic;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.DataAccess.Seeders
{
    public static class RecordStatusSeeder
    {
        public static List<RecordStatus> GetSeeds()
        {
            var list = new List<RecordStatus>()
            {
                new RecordStatus() {RecordStatusId = 1, RecordStatusName = "Neueintritt"},
                new RecordStatus() {RecordStatusId = 2, RecordStatusName = "Ergänzt"},
                new RecordStatus() {RecordStatusId = 3, RecordStatusName = "Wiedereintritt"},
                new RecordStatus() {RecordStatusId = 4, RecordStatusName = "Firmenwechsel"},
                new RecordStatus() {RecordStatusId = 5, RecordStatusName = "Abgeschlossen"}
            };
            return list;
        }
    }
}
