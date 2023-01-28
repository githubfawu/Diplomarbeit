using System.Collections.ObjectModel;
using VZEintrittsApp.Model;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;
using VZEintrittsApp.Model.RecordEntity;

namespace VZEintrittsApp.Test.Model
{
    public class MockRepository : IRepository
    {
        public ObservableCollection<Record> RecordsList { get; set; }

        public void GetAllClosedRecords()
        {

        }

        public void GetAllOpenRecords()
        {

        }

        public bool UpdateRecord(Record record, Employee employee)
        {
            return true;
        }

        public ObservableCollection<Note> GetAllNotes(string description)
        {
            return new ObservableCollection<Note>();
        }

        public Employee ReadAllAdAttributes(string abbreviation)
        {
            return new Employee();
        }

        public void ImportDocument(string file)
        {

        }

        public ObservableCollection<ActiveDirectoryGroup> GetAllAdGroups(string abbreviation)
        {
            return new ObservableCollection<ActiveDirectoryGroup>();
        }

        public ObservableCollection<RecordStatus> GetAllARecordStatus()
        {
            return new ObservableCollection<RecordStatus>();
        }

        public ObservableCollection<DirectReport> GetAllDirectReports(string managersAbbreviation, string abbreviation)
        {
            return new ObservableCollection<DirectReport>();
        }

        public ObservableCollection<ManagementLevel> GetAllManagementLevels()
        {
            return new ObservableCollection<ManagementLevel>();
        }

        public bool CopyRightsFromUser(string sourceAbbreviation, string targetAbbreviation)
        {
            return true;
        }

        public bool RemoveGroupFromUser(string abbreviation, string groupName)
        {
            return true;
        }

        public bool WriteSpecificAdAttribute(string employeeAttributeName, string value, Employee employee)
        {
            return true;
        }

        public string[] GetFreeNumberFromAd(string description)
        {
            return new string[]{};
        }

        public string GetCorrectNumberFormat(string number, string cityName)
        {
            return "string";
        }

        public byte[] GetOriginalDocument(string filename)
        {
            return new byte[0];
        }

        public ObservableCollection<SubsidiaryCompany> GetAllSubsidiaries()
        {
            return new ObservableCollection<SubsidiaryCompany>();
        }
    }
}
