using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace VZEintrittsApp.Import.PDFReader
{
    internal class ReadPdfDocument : IReadDocument
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
                        var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);

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

                            if (entity.Contains("Kürzel"))
                            {
                                if (employee != null)
                                {
                                    if (entity.Substring(8).Length == 4)
                                    {
                                        string abbreviation = CheckForEmptyStrings(entity, 8); ;
                                        employee.Abbreviation = abbreviation.Substring(0, 2) + abbreviation.Substring(2, 2).ToLower();
                                    }
                                    else
                                    {
                                        employee.Abbreviation = entity.Substring(8);
                                    }
                                }
                            }

                            if (entity.Contains("Vorname"))
                            {
                                if (employee != null) employee.Name = CheckForEmptyStrings(entity, 9);
                            }
                            if (entity.Contains("Name"))
                            {
                                if (employee != null) employee.LastName = CheckForEmptyStrings(entity, 6);
                            }
                            if (entity.Contains("Firmen E-Mail"))
                            {
                                if (employee != null) employee.MailAdress = CheckForEmptyStrings(entity, 15);
                            }
                            if (entity.Contains("Stellen-Nr"))
                            {
                                if (employee != null) employee.Title = CheckForEmptyStrings(entity, 21);
                            }
                            if (entity.Contains("Pensum"))
                            {
                                if (employee != null) employee.Workload = CheckForEmptyStrings(entity, 8);
                            }
                            if (entity.Contains("Geschäftsbereich"))
                            {
                                if (employee != null) employee.Company = CheckForEmptyStrings(entity, 18);
                            }
                            if (entity.Contains("Abteilungsname"))
                            {
                                if (employee != null) employee.Department = CheckForEmptyStrings(entity, 16);
                            }
                            if (entity.Contains("Standort"))
                            {
                                if (employee != null) employee.Street = CheckForEmptyStrings(entity, 10);
                                MessageBox.Show(employee.Street);
                            }
                        }
                    }
                }
            }
            return employeeList;
        }

        public string CheckForEmptyStrings(string property, int length)
        {
            if (property.Length > length)
            {
                return property.Substring(length);
            }
            return null;
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

                            if (entity.Contains("Kürzel"))
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

                            if (entity.Contains("Eintrittsdatum") && entity.Length <= 30)
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
