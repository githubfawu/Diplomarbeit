using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using VZEintrittsApp.Model.Domain;
using VZEintrittsApp.Model.EmployeeEntity;

namespace VZEintrittsApp.API.AD
{
    public partial class DirectoryServices
    {
        public bool WriteIndividualAttribute(string employeeAttributeName, string value, List<ManagementLevel> managementLevels, Employee employee)
        {
            try
            {
                if (employeeAttributeName == "VzManagementLevel.MgmtLevelId")
                {
                    foreach (var managementGroup in managementLevels)
                    {
                        if (!string.IsNullOrWhiteSpace(managementGroup.MgmtLevelGroupName))
                        {
                            RemoveGroupFromUser(employee.Abbreviation, managementGroup.MgmtLevelGroupName);
                        }
                    }
                    var managementLevel = managementLevels.Find(m => m.MgmtLevelId == Int32.Parse(value));
                    if (!string.IsNullOrWhiteSpace(managementLevel?.MgmtLevelGroupName))
                    {
                        AddManagementGroupToUser(employee.Abbreviation, managementLevel);
                    }
                }
                if (employeeAttributeName == "ExpirationDate")
                {
                    SetNewExpirationDate(employee);
                }
                if (employeeAttributeName == "Manager")
                {
                    return ChangeManager(employee.Abbreviation, employeeAttributeName, value);
                }
                else
                {
                    using var context = new PrincipalContext(ContextType.Domain);
                    {
                        UserPrincipal user = UserPrincipal.FindByIdentity(context, employee.Abbreviation);
                        DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject();
                        var attribute = AttributeList.FirstOrDefault(e => e.EmployeeAttributeName == employeeAttributeName);
                        if (attribute != null)
                        {
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                userEntry.Properties[attribute.ActiveDirectoryName].Clear();
                                userEntry.CommitChanges();
                                Log.Write(DateTime.Now,
                                    WindowsIdentity.GetCurrent().Name,
                                    employee.Abbreviation,
                                    ($"Das AD-Attribut {attribute.ActiveDirectoryName} wurde gelöscht"));
                            }
                            else
                            {
                                userEntry.Properties[attribute.ActiveDirectoryName].Value = value;
                                userEntry.CommitChanges();
                                Log.Write(DateTime.Now,
                                    WindowsIdentity.GetCurrent().Name,
                                    employee.Abbreviation,
                                    ($"Das AD-Attribut {attribute.ActiveDirectoryName} wurde mit dem Wert {value} geschrieben"));
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Log.Write(DateTime.Now,
                    WindowsIdentity.GetCurrent().Name,
                    employee.Abbreviation,
                    ($"Fehler beim schreiben des AD-Attributes {employeeAttributeName} mit dem Wert {value}"));
                return false;
            }
        }

        private bool ChangeManager(string abbreviation, string attributeName, string value)
        {
            try
            {
                using var context = new PrincipalContext(ContextType.Domain);
                {
                    UserPrincipal employee = UserPrincipal.FindByIdentity(context, abbreviation);
                    if (employee != null)
                    {
                        DirectoryEntry userEntry = (DirectoryEntry)employee.GetUnderlyingObject();
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            userEntry.Properties["manager"].Clear();
                            userEntry.CommitChanges();
                            Log.Write(DateTime.Now,
                                WindowsIdentity.GetCurrent().Name,
                                abbreviation,
                                ($"Das AD-Attribut {attributeName} wurde gelöscht"));
                            return true;
                        }
                        UserPrincipal manager = UserPrincipal.FindByIdentity(context, value);
                        if (manager != null)
                        {
                            var managersCName = manager.DistinguishedName;
                            userEntry.Properties["manager"].Value = managersCName;
                            userEntry.CommitChanges();
                            Log.Write(DateTime.Now,
                                WindowsIdentity.GetCurrent().Name,
                                abbreviation,
                                ($"Das AD-Attribut {attributeName} wurde mit dem Wert {value} geschrieben"));
                            return true;
                        }

                        MessageBox.Show($"Der Vorgesetzte mit dem Kürzel {value} wurde nicht gefunden.");
                        return false;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        private bool SetNewExpirationDate(Employee employee)
        {
            try
            {
                using var context = new PrincipalContext(ContextType.Domain);
                {
                    if (employee.ExpirationDate != null)
                    {
                        UserPrincipal user = UserPrincipal.FindByIdentity(context, employee.Abbreviation);
                        user.AccountExpirationDate = employee.ExpirationDate;
                        user.Save();
                        Log.Write(DateTime.Now,
                            WindowsIdentity.GetCurrent().Name,
                            employee.Abbreviation,
                            ($"Das AD-Attribut 'Konto läuft ab' wurde mit dem Wert {employee.ExpirationDate} geschrieben"));
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                throw;
            }
        }

        private bool SetManager(string employeeAbbreviation, string managersAbbreviation)
        {
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                if (UserPrincipal.FindByIdentity(context, managersAbbreviation) != null)
                {
                    UserPrincipal manager = UserPrincipal.FindByIdentity(context, managersAbbreviation);
                    var managersCName = manager.DistinguishedName;

                    UserPrincipal employee = UserPrincipal.FindByIdentity(context, employeeAbbreviation);
                    DirectoryEntry employeeEntry = (DirectoryEntry)employee.GetUnderlyingObject();
                    employeeEntry.Properties["manager"].Value = managersCName;
                    employeeEntry.CommitChanges();
                    Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, employeeAbbreviation, $"{managersAbbreviation} wurde als Vorgesetzter gesetzt");
                    return true;
                }
                MessageBox.Show($"Der Vorgesetzte {managersAbbreviation} für den Benutzer {employeeAbbreviation} konnte im AD nicht gefunden werden!");
                return false;
            }
        }

        public bool CopyRightsFromOtherUser(string sourceAbbreviation, string targetAbbreviation)
        {
            List<string> groupNames = new List<string>();
            try
            {
                using (PrincipalContext principalContextSource = new PrincipalContext(ContextType.Domain))
                {
                    using (UserPrincipal sourceUser = UserPrincipal.FindByIdentity(principalContextSource, IdentityType.SamAccountName, sourceAbbreviation))
                    {
                        foreach (GroupPrincipal groupcopy in sourceUser.GetGroups())
                        {
                            if (groupcopy.Name is not ("Domain Users" or "Domänen-Benutzer"))
                            {
                                groupNames.Add(groupcopy.Name);
                            }
                        }
                    }
                }
                using (PrincipalContext principalContextTarget = new PrincipalContext(ContextType.Domain))
                {
                    using (UserPrincipal targetUser = UserPrincipal.FindByIdentity(principalContextTarget, IdentityType.SamAccountName, targetAbbreviation))
                    {
                        foreach (var groupName in groupNames)
                        {
                            GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContextTarget, groupName);
                            Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, targetAbbreviation, $"Die Gruppe {groupName} wurde beim kopieren der Rechte von {sourceAbbreviation} hinzugefügt");
                            group.Members.Add(targetUser);
                            group.Save();
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public bool RemoveGroupFromUser(string abbreviation, string groupName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    using PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
                    using (GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContext, groupName))
                    {
                        UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, abbreviation);
                        if (group.Members.Contains(user))
                        {
                            group.Members.Remove(user);
                            group.Save();
                            Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, abbreviation, $"Die Gruppe {groupName} wurde entfernt");
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }
        public bool AddManagementGroupToUser(string abbreviation, ManagementLevel? managementLevel)
        {
            try
            {
                if (managementLevel == null || managementLevel.MgmtLevel.Contains("Keine"))
                {
                    return false;
                }
                using PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
                using (GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContext, managementLevel.MgmtLevelGroupName))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, abbreviation);
                    group.Members.Add(user);
                    group.Save();
                    Log.Write(DateTime.Now, WindowsIdentity.GetCurrent().Name, abbreviation, $"Die Gruppe {managementLevel.MgmtLevelGroupName} wurde hinzugefügt");
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
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

                    if (employee.ExpirationDate != null)
                    {
                        user.AccountExpirationDate = employee.ExpirationDate;
                    }
                    user.SetPassword("Salamander2000"); //PW ebenfalls für Optionen auslagern
                    user.Save();

                    DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject();
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
                    userEntry.Properties["wWWHomePage"].Value = employee.HomePage;
                    userEntry.Properties["vzAcademicTitle"].Value = employee.VzAcademicTitle;
                    userEntry.Properties["vzEmployeePensum"].Value = employee.VzPensum;
                    userEntry.Properties["vzEmployeeStartDate"].Value = employee.VzStartDate;
                    userEntry.Properties["vzBusinessUnitSupervisor"].Value = employee.VzBusinessUnitSupervisor;
                    userEntry.Properties["vzRegionalSupervisor"].Value = employee.VzRegionalSupervisor;
                    userEntry.Properties["vzBirthday"].Value = employee.VzBirthday;
                    userEntry.Properties["vzTeam"].Value = employee.VzTeam;
                    SetManager(employee.Abbreviation, employee.Manager);
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
