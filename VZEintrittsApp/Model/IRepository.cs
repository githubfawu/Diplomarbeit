using System.Collections.ObjectModel;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;
using VZEintrittsApp.Model.RecordEntity;

namespace VZEintrittsApp.Model;

public interface IRepository
{
    ObservableCollection<Record> RecordsList { get; set; }
    public void GetAllClosedRecords();
    public void GetAllOpenRecords();
    public bool UpdateRecord(Record record, Employee employee);
    public ObservableCollection<Note> GetAllNotes(string description);
    public Employee ReadAllAdAttributes(string abbreviation);
    public void ImportDocument(string file);
    public ObservableCollection<ActiveDirectoryGroup> GetAllAdGroups(string abbreviation);
    public ObservableCollection<RecordStatus> GetAllARecordStatus();
    public ObservableCollection<DirectReport> GetAllDirectReports(string managersAbbreviation, string abbreviation);
    public ObservableCollection<ManagementLevel> GetAllManagementLevels();
    public bool CopyRightsFromUser(string sourceAbbreviation, string targetAbbreviation);
    public bool RemoveGroupFromUser(string abbreviation, string groupName);
    public bool WriteSpecificAdAttribute(string employeeAttributeName, string value, Employee employee);
    public string[] GetFreeNumberFromAd(string description);
    public byte[] GetOriginalDocument(string filename);
    public ObservableCollection<SubsidiaryCompany> GetAllSubsidiaries();
}