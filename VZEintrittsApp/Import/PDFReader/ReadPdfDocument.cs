using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import.PDFReader
{
    public class ReadPdfDocument : IReadDocument
    {
        public List<Employee> ReadUsers(string file)
        {
            List<Employee> employeeList = new List<Employee>();

            using (var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions()))
            {
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using (var pageReader = docReader.GetPageReader(i))
                    {
                        Employee? employee = null;

                        var text = pageReader.GetText();
                        var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase).ToList();

                        foreach (var entity in splitString)
                        {
                            if (entity.Contains("Mitarb.-Nr.:"))
                            {
                                var employeeNrString = entity.Substring(13);
                                var employeeNr = int.Parse(employeeNrString);

                                var newEmployee = new Employee()
                                {
                                    EmployeeNr = employeeNr
                                };
                                employeeList.Add(newEmployee);
                                employee = newEmployee;
                            }

                            if (employee != null)
                            {
                                if (entity.Contains("Kürzel:"))
                                {
                                    string abbreviation = CheckForNullAndNewLines(entity, 8, splitString);
                                    employee.Abbreviation = CorrectUpperLower(abbreviation);
                                }

                                if (entity.Contains("Vorname:"))
                                {
                                    employee.FirstName = CheckForNullAndNewLines(entity, 9, splitString);
                                }
                                if (entity.Contains("Name:"))
                                {
                                    employee.LastName = CheckForNullAndNewLines(entity, 6, splitString);
                                }
                                if (entity.Contains("Rufname:"))
                                {
                                    employee.CallSignName = CheckForNullAndNewLines(entity, 9, splitString);
                                }
                                if (entity.Contains("Rufnachname:"))
                                {
                                    employee.CallSignLastName = CheckForNullAndNewLines(entity, 13, splitString);
                                }
                                if (entity.Contains("Firmen E-Mail:"))
                                {
                                    employee.MailAdress = CheckForNullAndNewLines(entity, 15, splitString);
                                }
                                if (entity.Contains("Stellen-Nr.:"))
                                {
                                    employee.Position = CheckForNullAndNewLines(entity, 21, splitString);
                                }
                                if (entity.Contains("Pensum:"))
                                {
                                    var pensum = CheckForNullAndNewLines(entity, 8, splitString);
                                    employee.VzPensum = RemoveSpecialCharacters(pensum);
                                }
                                if (entity.Contains("Geschäftsbereich:"))
                                {
                                    employee.BusinessArea = CheckForNullAndNewLines(entity, 18, splitString);
                                }
                                if (entity.Contains("Unternehmen:"))
                                {
                                    employee.Company = CheckForNullAndNewLines(entity, 13, splitString);
                                    if (string.IsNullOrWhiteSpace(employee.Company)) employee.Company = employee.BusinessArea;
                                }
                                if (entity.Contains("Abteilungsname:"))
                                {
                                    employee.Department = CheckForNullAndNewLines(entity, 16, splitString);
                                }
                                if (entity.Contains("Titel in Mailfuss:"))
                                {  
                                    employee.TitleInMailFooter = CheckForTitleInMailfooter(CheckForNullAndNewLines(entity, 19, splitString));
                                }
                                if (entity.Contains("Titel 1:"))
                                {
                                    employee.VzAcademicTitle = CheckForNullAndNewLines(entity, 9, splitString);
                                }
                                if (entity.Contains("erster Arbeitstag:"))
                                {
                                    employee.VzStartDate = CheckForNullAndNewLines(entity, 19, splitString);
                                }
                                if (entity.Contains("Rang:"))
                                {
                                    employee.VzGrade = CheckForNullAndNewLines(entity, 6, splitString);
                                }
                                if (entity.Contains("Bereichsleiter:"))
                                {
                                    employee.VzBusinessUnitSupervisor = CheckForNullAndNewLines(entity, 16, splitString);
                                }
                                if (entity.Contains("Regionenleiter:"))
                                {
                                    employee.VzRegionalSupervisor = CheckForNullAndNewLines(entity, 16, splitString);
                                }
                                if (entity.Contains("Kaderstufe:"))
                                {
                                    employee.VzManagementLevel = new ManagementLevel(){MgmtLevel = CheckForNullAndNewLines(entity, 12, splitString)};
                                }
                                if (entity.Contains("Austrittsdatum:"))
                                {
                                    var value = CheckForNullAndNewLines(entity, 16, splitString);
                                    if (value != null) employee.ExpirationDate = DateTime.ParseExact(value, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                }
                                if (entity.Contains("Geburtstag:"))
                                {
                                    employee.VzBirthday = CheckForNullAndNewLines(entity, 12, splitString);
                                }
                                if (entity.Contains("Vorgesetzter:") && !entity.Contains("dir. Vorgesetzter:"))
                                {
                                    employee.Manager = CheckForNullAndNewLines(entity, 14, splitString);
                                    if (employee.Manager != null)
                                        employee.Manager = CleanManagerString(employee.Manager);
                                }

                                if (entity.Contains("Standort:"))
                                {
                                    var entityString = CheckForNullAndNewLines(entity, 10, splitString);
                                    if (entityString != null)
                                    {
                                        string[] adressParts = ExtractAdressFromEntity(entityString);
                                        employee.City = adressParts[0];
                                        employee.PostalCode = adressParts[1];
                                        employee.Street = adressParts[2];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return employeeList;
        }

        private string CleanManagerString(string employeesManager)
        {
            if (employeesManager.Contains("Zeitachsen-Datum"))
            {
                return Regex.Replace(employeesManager, @" Zeitachsen-Datum", "");
            }
            return employeesManager;
        }

        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private string? CheckForNullAndNewLines(string entity, int length, List<string> list)
        {
            if (entity.Length > length)
            {
                if ((list.IndexOf(entity) + 1) <= list.Count)
                {
                    var index = list.IndexOf(entity) + 1;
                    if (!list[index].Contains(":"))
                    {
                        return (entity.Substring(length) + " " + list[index]);
                    }
                    return entity.Substring(length);
                }
                return null;
            }
            return null;
        }

        private string [] ExtractAdressFromEntity(string completeAdress)
        {
            return completeAdress.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool CheckForTitleInMailfooter(string value)
        {
            if (value.Contains("Ja"))
            {
                return true;
            }
            return false;
        }

        private string CorrectUpperLower(string abbreviation)
        {
            return abbreviation.Length switch
            {
                4 => abbreviation.Substring(0, 2).ToUpper() + abbreviation.Substring(2, 2).ToLower(),
                3 => abbreviation.Substring(0, 2) + abbreviation.Substring(2, 1).ToLower(),
                2 => abbreviation.Substring(0, 2).ToUpper(),
                _ => abbreviation
            };
        }


        public List<Record> ReadRecords(string file)
        {
            List<Record> recordsList = new List<Record>();

            using (var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions()))
            {
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using (var pageReader = docReader.GetPageReader(i))
                    {
                        Record? record = null;

                        var text = pageReader.GetText();
                        var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);

                        foreach (var entity in splitString)
                        {
                            if (entity.Contains("Mitarb.-Nr.:"))
                            {
                                var employeeNrString = entity.Substring(13);
                                var employeeNr = int.Parse(employeeNrString);

                                var newRecord = new Record()
                                {
                                    EmployeeNr = employeeNr
                                };
                                recordsList.Add(newRecord);
                                record = newRecord;
                            }

                            if (entity.Contains("Kürzel:"))
                            {
                                if (record != null)
                                {
                                    if (entity.Substring(8).Length == 4)
                                    {
                                        string abbreviation = entity.Substring(8);
                                        record.Abbreviation = abbreviation.Substring(0, 2) + abbreviation.Substring(2, 2).ToLower();
                                    }
                                    else
                                    {
                                        record.Abbreviation = entity.Substring(8);
                                    }
                                }
                            }

                            if (entity.Contains("erster Arbeitstag:"))
                            {
                                DateTime FirstWorkingDay = DateTime.Parse(entity.Substring(19));
                                if (record != null) record.FirstWorkingDay = FirstWorkingDay;

                            }

                            if (entity.Contains("Eintrittsdatum:"))
                            {
                                DateTime EntryDate = DateTime.Parse(entity.Substring(16));
                                if (record != null) record.EntryDate = EntryDate;

                            }
                        }
                    }
                }
            }
            return recordsList;
        }
    }
}
