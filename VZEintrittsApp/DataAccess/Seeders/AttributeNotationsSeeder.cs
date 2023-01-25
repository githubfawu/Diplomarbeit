
using System.Collections.Generic;
using VZEintrittsApp.Model.ActiveDirectory;

namespace VZEintrittsApp.DataAccess.Seeders
{
    public static class AttributeNotationsSeeder
    {
        public static List<AttributeNotations> GetSeeds()
        {
            var list = new List<AttributeNotations>()
            {
                new AttributeNotations(){NotationId = 1, EmployeeAttributeName = "NameOfAdObject", ActiveDirectoryName = "CN"},
                new AttributeNotations(){NotationId = 2, EmployeeAttributeName = "DisplayName", ActiveDirectoryName = "displayName"},
                new AttributeNotations(){NotationId = 3, EmployeeAttributeName = "FirstName", ActiveDirectoryName = "givenName"},
                new AttributeNotations(){NotationId = 4, EmployeeAttributeName = "LastName", ActiveDirectoryName = "sn"},
                new AttributeNotations(){NotationId = 5, EmployeeAttributeName = "MailAdress", ActiveDirectoryName = "mail"},
                new AttributeNotations(){NotationId = 6, EmployeeAttributeName = "Department", ActiveDirectoryName = "department"},
                new AttributeNotations(){NotationId = 7, EmployeeAttributeName = "Position", ActiveDirectoryName = "title"},
                new AttributeNotations(){NotationId = 8, EmployeeAttributeName = "Company", ActiveDirectoryName = "company"},
                new AttributeNotations(){NotationId = 9, EmployeeAttributeName = "Street", ActiveDirectoryName = "streetAddress"},
                new AttributeNotations(){NotationId = 10, EmployeeAttributeName = "City", ActiveDirectoryName = "l"},
                new AttributeNotations(){NotationId = 11, EmployeeAttributeName = "State", ActiveDirectoryName = "st"},
                new AttributeNotations(){NotationId = 12, EmployeeAttributeName = "PostalCode", ActiveDirectoryName = "postalCode"},
                new AttributeNotations(){NotationId = 13, EmployeeAttributeName = "Office", ActiveDirectoryName = "physicalDeliveryOfficeName"},
                new AttributeNotations(){NotationId = 14, EmployeeAttributeName = "Description", ActiveDirectoryName = "description"},
                new AttributeNotations(){NotationId = 15, EmployeeAttributeName = "PostOfficeBox", ActiveDirectoryName = "postOfficeBox"},
                new AttributeNotations(){NotationId = 16, EmployeeAttributeName = "Country", ActiveDirectoryName = "c"},
                new AttributeNotations(){NotationId = 17, EmployeeAttributeName = "Pager", ActiveDirectoryName = "pager"},
                new AttributeNotations(){NotationId = 18, EmployeeAttributeName = "OtherTelephone", ActiveDirectoryName = "otherTelephone"},
                new AttributeNotations(){NotationId = 19, EmployeeAttributeName = "TelephoneNumber", ActiveDirectoryName = "telephoneNumber"},
                new AttributeNotations(){NotationId = 20, EmployeeAttributeName = "IpPhoneNumber", ActiveDirectoryName = "ipPhone"},
                new AttributeNotations(){NotationId = 21, EmployeeAttributeName = "FaxNumber", ActiveDirectoryName = "facsimileTelephoneNumber"},
                new AttributeNotations(){NotationId = 22, EmployeeAttributeName = "MobileNumber", ActiveDirectoryName = "mobile"},
                new AttributeNotations(){NotationId = 23, EmployeeAttributeName = "VzAcademicTitle", ActiveDirectoryName = "vzAcademicTitle"},
                new AttributeNotations(){NotationId = 24, EmployeeAttributeName = "VzPensum", ActiveDirectoryName = "vzEmployeePensum"},
                new AttributeNotations(){NotationId = 25, EmployeeAttributeName = "VzBirthday", ActiveDirectoryName = "vzBirthday"},
                new AttributeNotations(){NotationId = 26, EmployeeAttributeName = "VzStartDate", ActiveDirectoryName = "vzEmployeeStartDate"},
                new AttributeNotations(){NotationId = 27, EmployeeAttributeName = "VzBusinessUnitSupervisor", ActiveDirectoryName = "vzBusinessUnitSupervisor"},
                new AttributeNotations(){NotationId = 28, EmployeeAttributeName = "VzRegionalSupervisor", ActiveDirectoryName = "vzRegionalSupervisor"},
                new AttributeNotations(){NotationId = 29, EmployeeAttributeName = "HomePage", ActiveDirectoryName = "wWWHomePage"},
                new AttributeNotations(){NotationId = 30, EmployeeAttributeName = "ExtensionAttribute1", ActiveDirectoryName = "extensionAttribute1"},
                new AttributeNotations(){NotationId = 31, EmployeeAttributeName = "ExtensionAttribute15", ActiveDirectoryName = "extensionAttribute15"},
                new AttributeNotations(){NotationId = 32, EmployeeAttributeName = "Manager", ActiveDirectoryName = "manager"}
            };
            return list;
        }
    }
}

