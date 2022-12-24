﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Windows;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.API.AD
{
    public class DirectoryServices
    { 
        public DirectoryServices() { }

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
                        MailAdress = user.EmailAddress
                    };
                    DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject();
                    employee.Company = userEntry.Properties["Company"].Value?.ToString();
                    employee.Department = userEntry.Properties["Department"].Value?.ToString();
                    employee.Position = userEntry.Properties["title"].Value?.ToString();
                    employee.City = userEntry.Properties["l"].Value?.ToString();
                    employee.PostalCode = userEntry.Properties["postalCode"].Value?.ToString();
                    employee.Street = userEntry.Properties["streetAddress"].Value?.ToString();
                    employee.State = userEntry.Properties["st"].Value?.ToString();
                    employee.Country = userEntry.Properties["c"].Value?.ToString();
                    employee.VzAcademicTitle = userEntry.Properties["vzAcademicTitle"].Value?.ToString();
                    employee.VzPensum = userEntry.Properties["vzEmployeePensum"].Value?.ToString();

                    return employee;
                }
            }
            MessageBox.Show("Es existiert im AD kein Benutzer mit diesem Kürzel.");
            return null;
        }

        public bool CreateNewAdAccount(List<Employee> employeeList)
        {
            foreach (var employee in employeeList)
            {
                using (var context = new PrincipalContext(ContextType.Domain, "vz.ch", "OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch"))//Pfade auslagern (in DB?)
                using (var user = new UserPrincipal(context)
                       {
                           Name = $"{employee.FirstName} {employee.LastName}",
                           UserPrincipalName = employee.Abbreviation + "@vzch.com",
                           SamAccountName = employee.Abbreviation,
                           Surname = employee.LastName,
                           GivenName = employee.FirstName,
                           EmailAddress = employee.MailAdress,
                           DisplayName = $"{employee.FirstName} {employee.LastName}",
                           Enabled = false
                       })
                {
                    user.SetPassword("Sonne100");
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
                    userEntry.Properties["vzAcademicTitle"].Value = employee.VzAcademicTitle;
                    userEntry.Properties["vzEmployeePensum"].Value = employee.VzPensum;
                    userEntry.CommitChanges();
                }
            }
            return true;
        }
    }
}
