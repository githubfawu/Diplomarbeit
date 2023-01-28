using VZEintrittsApp.API.AD;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.Test.DirectoryServices
{
    public class MockDirectoryServices : IDirectoryServices
    {
        public bool CheckIfUserExists(string abbreviation)
        {
            return false;
        }

        public List<ActiveDirectoryGroup> GetAdGroupsFromUser(string abbreviation)
        {
            return new List<ActiveDirectoryGroup>();
        }

        public bool IsNumberFree(long iPPhoneNumber)
        {
            return true;
        }

        public Employee GetAttributes(string abbreviation, List<ManagementLevel> managementLevels)
        {
            return new Employee();
        }
        public List<DirectReport> GetDirectReports(string managersAbbreviation, string abbreviation)
        {
            return new List<DirectReport>();
        }
        public bool WriteIndividualAttribute(string employeeAttributeName, string value, List<ManagementLevel> managementLevels, Employee employee)

        {
            return true;
        }

        public bool CopyRightsFromOtherUser(string sourceAbbreviation, string targetAbbreviation)
        {
            return true;
        }

        public bool RemoveGroupFromUser(string abbreviation, string groupName)
        {
            return true;
        }

        public bool AddManagementGroupToUser(string abbreviation, ManagementLevel? managementLevel)
        {
            return true;
        }

        public bool CreateNewAdAccount(Employee employee)
        {
            return true;
        }
    }
}
