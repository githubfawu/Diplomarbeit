using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.API.AD
{
    public class DirectoryServices
    {
        private LoggerContext Log;
        private AttributeNotationContext AttributeNotationContext;
        private List<AttributeNotations> AttributeList;

        public DirectoryServices(LoggerContext loggerContext, AttributeNotationContext attributeNotationContext)
        {
            Log = loggerContext;
            AttributeNotationContext = attributeNotationContext;
            AttributeList = AttributeNotationContext.GetAllEntries();
        }

        public bool CheckIfUserExists (string abbreviation)
        {
            try
            {
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, abbreviation);

                if (user != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool WriteIndividualAttribute(string employeeAttributeName, string abbreviation, string value)
        {
            try
            {
                using var context = new PrincipalContext(ContextType.Domain, "vz.ch", "OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch");
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, abbreviation);
                    DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject();
                    var attribute = AttributeList.FirstOrDefault(e => e.EmployeeAttributeName == employeeAttributeName);
                    userEntry.Properties[attribute.ActiveDirectoryName].Value = value;
                    userEntry.CommitChanges();
                    Log.Write(DateTime.Now,
                        WindowsIdentity.GetCurrent().Name,
                        abbreviation, 
                        ($"Das AD-Attribut {attribute.ActiveDirectoryName} wurde mit dem Wert {value} geschrieben"));
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Log.Write(DateTime.Now,
                    WindowsIdentity.GetCurrent().Name,
                    abbreviation,
                    ($"Fehler beim schreiben des AD-Attributes {employeeAttributeName} mit dem Wert {value}"));
                return false;
            }
        }

        public bool IsNumberFreeChecker(long iPPhoneNumber)
        {
            var entry = new DirectoryEntry("LDAP://OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch");
            using (DirectorySearcher dsSearcher = new DirectorySearcher(entry))
            {
                dsSearcher.Filter = $"(&(objectClass=user)(objectCategory=person)(ipPhone=+{iPPhoneNumber}))";
                SearchResult result = dsSearcher.FindOne();
                return result == null;
            }
        }

        public Employee GetAttributes(string abbreviation)
        {
            if (CheckIfUserExists(abbreviation))
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, abbreviation);
                    Employee employee = new Employee()
                    {
                        Abbreviation = user.SamAccountName,
                        NameOfAdObject = user.Name,
                        FirstName = user.GivenName,
                        LastName = user.Surname,
                        DisplayName = user.DisplayName,
                        MailAdress = user.EmailAddress,
                        Description = user.Description
                    };
                    DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject();
                    employee.Company = userEntry.Properties["Company"].Value?.ToString();
                    employee.Department = userEntry.Properties["Department"].Value?.ToString();
                    employee.Position = userEntry.Properties["title"].Value?.ToString();
                    employee.City = userEntry.Properties["l"].Value?.ToString();
                    employee.PostalCode = userEntry.Properties["postalCode"].Value?.ToString();
                    employee.Office = userEntry.Properties["physicalDeliveryOfficeName"].Value?.ToString();
                    employee.Street = userEntry.Properties["streetAddress"].Value?.ToString();
                    employee.State = userEntry.Properties["st"].Value?.ToString();
                    employee.Country = userEntry.Properties["c"].Value?.ToString();
                    employee.TelephoneNumber = userEntry.Properties["telephoneNumber"].Value?.ToString();
                    employee.Pager = userEntry.Properties["pager"].Value?.ToString();
                    employee.OtherTelephone = userEntry.Properties["otherTelephone"].Value?.ToString();
                    employee.FaxNumber = userEntry.Properties["facsimileTelephoneNumber"].Value?.ToString();
                    employee.IpPhoneNumber = userEntry.Properties["ipPhone"].Value?.ToString();
                    employee.VzAcademicTitle = userEntry.Properties["vzAcademicTitle"].Value?.ToString();
                    employee.VzPensum = userEntry.Properties["vzEmployeePensum"].Value?.ToString();
                    employee.VzStartDate = userEntry.Properties["vzEmployeeStartDate"].Value?.ToString();
                    employee.VzBusinessUnitSupervisor = userEntry.Properties["vzBusinessUnitSupervisor"].Value?.ToString();
                    employee.VzRegionalSupervisor = userEntry.Properties["vzRegionalSupervisor"].Value?.ToString();
                    employee.VzBirthday = userEntry.Properties["vzBirthday"].Value?.ToString();
                    return employee;
                }
            }
            MessageBox.Show("Es existiert im AD kein Benutzer mit diesem Kürzel.");
            return null;
        }

        public bool CreateNewAdAccount(Employee employee)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, "vz.ch",
                           "OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch")) //Pfade auslagern (in DB?)
                using (var user = new UserPrincipal(context)
                       {
                           Name = $"{employee.FirstName} {employee.LastName}",
                           UserPrincipalName = employee.Abbreviation + "@vzch.com",
                           SamAccountName = employee.Abbreviation,
                           Surname = employee.LastName,
                           GivenName = employee.FirstName,
                           EmailAddress = employee.MailAdress,
                           DisplayName = $"{employee.FirstName} {employee.LastName}",
                           Description = employee.Description,
                           Enabled = false
                       })
                {
                    user.SetPassword("Salamander2000"); //PW ebenfalls für Optionen auslagern
                    user.Save();

                    DirectoryEntry userEntry = (DirectoryEntry) user.GetUnderlyingObject();
                    userEntry.Properties["Company"].Value = employee.Company;
                    userEntry.Properties["Department"].Value = employee.Department;
                    userEntry.Properties["title"].Value = employee.Position;
                    userEntry.Properties["l"].Value = employee.City;
                    userEntry.Properties["postalCode"].Value = employee.PostalCode;
                    userEntry.Properties["streetAddress"].Value = employee.Street;
                    userEntry.Properties["st"].Value = employee.State;
                    userEntry.Properties["c"].Value = employee.Country;
                    userEntry.Properties["physicalDeliveryOfficeName"].Value = employee.Office;
                    userEntry.Properties["pager"].Value = employee.Pager;
                    userEntry.Properties["otherTelephone"].Value = employee.OtherTelephone;
                    userEntry.Properties["facsimileTelephoneNumber"].Value = employee.FaxNumber;
                    userEntry.Properties["ipPhone"].Value = employee.IpPhoneNumber;
                    userEntry.Properties["vzAcademicTitle"].Value = employee.VzAcademicTitle;
                    userEntry.Properties["vzEmployeePensum"].Value = employee.VzPensum;
                    userEntry.Properties["vzEmployeeStartDate"].Value = employee.VzStartDate;
                    userEntry.Properties["vzBusinessUnitSupervisor"].Value = employee.VzBusinessUnitSupervisor;
                    userEntry.Properties["vzRegionalSupervisor"].Value = employee.VzRegionalSupervisor;
                    userEntry.Properties["vzBirthday"].Value = employee.VzBirthday;
                    userEntry.CommitChanges();

                    Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, employee.Abbreviation, "Ein neues AD-Benutzerkonto wurde erstellt");
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
