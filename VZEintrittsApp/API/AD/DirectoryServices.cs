using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Windows;
using VZEintrittsApp.DataAccess.Contexts;
using VZEintrittsApp.Model.ActiveDirectory;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.API.AD
{
    public partial class DirectoryServices : IDirectoryServices
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

        public List<ActiveDirectoryGroup> GetAdGroupsFromUser(string abbreviation)
        {
            var result = new List<ActiveDirectoryGroup>();
            using (UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), IdentityType.SamAccountName, abbreviation))
                foreach (GroupPrincipal group in user.GetGroups())
                {
                    result.Add(new ActiveDirectoryGroup()
                    {
                        AdGroupName = group.Name,
                        AdGroupDescription = group.Description
                    });
                }
            return result;
        }

        public bool IsNumberFree(long iPPhoneNumber)
        {
            var entry = new DirectoryEntry("LDAP://OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch");
            using (DirectorySearcher dsSearcher = new DirectorySearcher(entry))
            {
                dsSearcher.Filter = $"(&(objectClass=user)(objectCategory=person)(ipPhone=+{iPPhoneNumber}))";
                SearchResult result = dsSearcher.FindOne();
                return result == null;
            }
        }

        public Employee GetAttributes(string abbreviation, List<ManagementLevel> managementLevels)
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
                    if (user.AccountExpirationDate != null) employee.ExpirationDate = user.AccountExpirationDate.Value;
                    employee.Company = userEntry.Properties["Company"].Value?.ToString();
                    employee.Department = userEntry.Properties["Department"].Value?.ToString();
                    employee.Position = userEntry.Properties["title"].Value?.ToString();
                    employee.City = userEntry.Properties["l"].Value?.ToString();
                    employee.PostalCode = userEntry.Properties["postalCode"].Value?.ToString();
                    employee.Office = userEntry.Properties["physicalDeliveryOfficeName"].Value?.ToString();
                    employee.Street = userEntry.Properties["streetAddress"].Value?.ToString();
                    employee.State = userEntry.Properties["st"].Value?.ToString();
                    employee.Country = userEntry.Properties["c"].Value?.ToString();
                    employee.HomePage = userEntry.Properties["wWWHomePage"].Value?.ToString();
                    employee.TelephoneNumber = userEntry.Properties["telephoneNumber"].Value?.ToString();
                    employee.Manager = SearchManager(userEntry.Properties["manager"].Value?.ToString());
                    employee.Pager = userEntry.Properties["pager"].Value?.ToString();
                    employee.OtherTelephone = userEntry.Properties["otherTelephone"].Value?.ToString();
                    employee.FaxNumber = userEntry.Properties["facsimileTelephoneNumber"].Value?.ToString();
                    employee.IpPhoneNumber = userEntry.Properties["ipPhone"].Value?.ToString();
                    employee.MobileNumber = userEntry.Properties["mobile"].Value?.ToString();
                    employee.VzAcademicTitle = userEntry.Properties["vzAcademicTitle"].Value?.ToString();
                    employee.VzPensum = userEntry.Properties["vzEmployeePensum"].Value?.ToString();
                    employee.VzStartDate = userEntry.Properties["vzEmployeeStartDate"].Value?.ToString();
                    employee.VzBusinessUnitSupervisor = userEntry.Properties["vzBusinessUnitSupervisor"].Value?.ToString();
                    employee.VzRegionalSupervisor = userEntry.Properties["vzRegionalSupervisor"].Value?.ToString();
                    employee.VzBirthday = userEntry.Properties["vzBirthday"].Value?.ToString();
                    employee.VzTeam = userEntry.Properties["vzTeam"].Value?.ToString();
                    employee.ExtensionAttribute1 = userEntry.Properties["extensionAttribute1"].Value?.ToString();
                    employee.ExtensionAttribute15 = userEntry.Properties["extensionAttribute15"].Value?.ToString();
                    employee.VzManagementLevel = ReadManagementLevel(employee.Abbreviation, managementLevels);
                    return employee;
                }
            }
            MessageBox.Show("Es existiert im AD kein Benutzer mit diesem Kürzel.");
            return null;
        }

        private string SearchManager(string managerCn)
        {
            try
            {
                if (managerCn != null)
                {
                    using (var context = new PrincipalContext(ContextType.Domain))
                    {
                        if (UserPrincipal.FindByIdentity(context, managerCn) != null)
                        {
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, managerCn);
                            return user.SamAccountName;
                        }
                        MessageBox.Show($"Der Benutzer {managerCn} wurde nicht gefunden.");
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        private ManagementLevel ReadManagementLevel(string abbreviation, List<ManagementLevel> managementLevels)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(context, abbreviation);

                foreach (var managementLevel in managementLevels)
                {
                    if (managementLevel.MgmtLevelGroupName != "")
                    {
                        using (GroupPrincipal group = GroupPrincipal.FindByIdentity(context, managementLevel.MgmtLevelGroupName))
                        {
                            if (group.Members.Contains(user))
                                return managementLevel;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            { 
                MessageBox.Show(ex.ToString());
            }
            return managementLevels.Find(m => m.MgmtLevel == "Keine");
        }

        public List<DirectReport> GetDirectReports(string managersAbbreviation, string abbreviation)
        {
            List<DirectReport> ListDirectReports = new List<DirectReport>();
            if (!String.IsNullOrWhiteSpace(managersAbbreviation))
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    using (UserPrincipal manager = UserPrincipal.FindByIdentity(context, managersAbbreviation))
                    {
                        DirectoryEntry managerEntry = (DirectoryEntry)manager.GetUnderlyingObject();
                        foreach (var directReport in managerEntry.Properties["directReports"])
                        {
                            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.DistinguishedName, directReport.ToString());

                            if (user != null)
                            {
                                if (user.SamAccountName != abbreviation)
                                {
                                    using (DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry)
                                    {
                                        var directReportEntity = new DirectReport()
                                        {
                                            SamAccountName = de.Properties["samAccountName"].Value as string,
                                            DisplayName = de.Properties["displayName"].Value as string,
                                            Position = $"Titel: {de.Properties["title"].Value}",
                                            StartDate = $"Eintrittsdatum: {de.Properties["vzEmployeeStartDate"].Value}"
                                        };
                                        ListDirectReports.Add(directReportEntity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ListDirectReports;
        }
    }
}
