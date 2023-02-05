using VZEintrittsApp.Model.RecordEntity;

namespace VZEintrittsApp.Test.Model
{
    public static class MockRecord
    {
        public static Record GetRecord()
        {
            var record = new Record()
            {
                Abbreviation = "Test",
                AssociatedFile = "File",
                EmployeeNr = 999999,
                EntryDate = DateTime.Now.Date,
                FirstWorkingDay = DateTime.Now.Date,
                RecordReadDate = DateTime.Now.Date,
                Language = "de",
                Recorder = "TestUser",
                RecordId = 999999
            };

            return record;
        }
    }
}
