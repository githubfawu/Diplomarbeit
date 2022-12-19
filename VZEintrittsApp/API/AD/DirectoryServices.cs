using System;
using System.Collections.Generic;
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
                        Name = user.GivenName,
                        LastName = user.Surname,
                        DisplayName = user.DisplayName,
                        MailAdress = user.EmailAddress
                    };
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
                using (var context = new PrincipalContext(ContextType.Domain, "vz.ch", "OU=Standarduser,OU=VZ_Users,DC=vz,DC=ch"))
                using (var user = new UserPrincipal(context)
                       {
                           UserPrincipalName = employee.Abbreviation,
                           SamAccountName = employee.Abbreviation,
                           Surname = employee.LastName,
                           GivenName = employee.Name,
                           EmailAddress = employee.MailAdress,
                           DisplayName = $"{employee.Name} {employee.LastName}",
                           Enabled = false
                       })
                {
                    user.SetPassword("Sonne100");
                    user.Save();
                }
            }
            return true;
        }
    }
}
