using System.DirectoryServices;
using System.Windows;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.API.AD
{
    public class DirectoryServices
    {
        private string DomainPath { get; set; }
        private DirectoryServicesHelper Helper { get; set; }
        public DirectoryServices()
        {
            DomainPath = GetCurrentDomainPath();
            Helper = new DirectoryServicesHelper();
        }

        public bool CheckIfUserExists (string abbreviation)
        {
            DirectorySearcher directorySearcher = null;
            DirectoryEntry directoryEntry = new DirectoryEntry(DomainPath);

            directorySearcher = Helper.UserSearcher(directoryEntry);
            directorySearcher.Filter = "(&(objectCategory=User)(objectClass=person)(sAMAccountName=" + abbreviation + "*))";
            var searchResult = directorySearcher.FindOne();

            if (searchResult != null)
            {
                return true;
            }
            return false;
        }

        public Employee GetAttributes(string abbreviation)
        {
            if (CheckIfUserExists(abbreviation))
            {
                return Helper.GetAdAttributes(DomainPath, abbreviation);
            }
            MessageBox.Show("Es existiert im AD kein Benutzer mit diesem Kürzel.");
            return null;
        }

        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

        
    }
}
