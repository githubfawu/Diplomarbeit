using System.Collections.Generic;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.API.AD;

public interface IDirectoryServices
{
    public bool CheckIfUserExists(string abbreviation);
    public List<ActiveDirectoryGroup> GetAdGroupsFromUser(string abbreviation);
    public bool IsNumberFree(long iPPhoneNumber);
    public Employee GetAttributes(string abbreviation, List<ManagementLevel> managementLevels);
    public List<DirectReport> GetDirectReports(string managersAbbreviation, string abbreviation);
    public bool WriteIndividualAttribute(string employeeAttributeName, string value,
        List<ManagementLevel> managementLevels, Employee employee);
    public bool CopyRightsFromOtherUser(string sourceAbbreviation, string targetAbbreviation);
    public bool RemoveGroupFromUser(string abbreviation, string groupName);
    public bool AddManagementGroupToUser(string abbreviation, ManagementLevel? managementLevel);
    public bool CreateNewAdAccount(Employee employee);

}