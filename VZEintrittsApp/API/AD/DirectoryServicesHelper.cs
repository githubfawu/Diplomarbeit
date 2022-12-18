

using System.DirectoryServices;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.API.AD
{
    public class DirectoryServicesHelper
    {
        public DirectoryServicesHelper() { }

        public Employee GetAdAttributes(string domainPath, string abbreviation)
        {
            DirectorySearcher directorySearcher = null;
            DirectoryEntry directoryEntry = new DirectoryEntry(domainPath);

            directorySearcher = UserSearcher(directoryEntry);
            directorySearcher.Filter = "(&(objectCategory=User)(objectClass=person)(userPrincipalName=" + abbreviation + "*))";
            var searchResult = directorySearcher.FindOne();

            Employee employee = new Employee()
            {
                Abbreviation = GetPropertyValue(searchResult, "sAMAccountName"),
                Name = GetPropertyValue(searchResult, "givenName"),
                LastName = GetPropertyValue(searchResult, "sn"),
                DisplayName = GetPropertyValue(searchResult, "displayName"),
                MailAdress = GetPropertyValue(searchResult, "mail")
            };

            return employee;
        }

        public DirectorySearcher UserSearcher(DirectoryEntry de)
        {
            DirectorySearcher ds = null;
            ds = new DirectorySearcher(de);
            return ds;
        }

        public string? GetPropertyValue(SearchResult sr, string propertyName)
        {
            string? ret = string.Empty;

            if (sr.Properties[propertyName].Count > 0)
                ret = sr.Properties[propertyName][0].ToString();

            return ret;
        }
    }
}
