using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.DataAccess;
using VZEintrittsApp.Domain;
using VZEintrittsApp.Model;

namespace VZEintrittsApp.Import.PDFReader
{
    internal class ReadPdfDocument : IReadDocument
    {
        private ContextHelper ContextHelper { get; set; }

        private FinalizeEmployee Finalize { get; set; }

    public ReadPdfDocument (ContextHelper contextHelper)
        {
            this.ContextHelper = contextHelper;
            Finalize = new FinalizeEmployee(contextHelper);
        }
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
                                        string abbreviation = CheckForNullValues(entity, 8); ;
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
                                if (employee != null) employee.FirstName = CheckForNullValues(entity, 9);
                            }
                            if (entity.Contains("Name"))
                            {
                                if (employee != null) employee.LastName = CheckForNullValues(entity, 6);
                            }
                            if (entity.Contains("Rufname"))
                            {
                                if (employee != null) employee.CallSignName = CheckForNullValues(entity, 9);
                            }
                            if (entity.Contains("Rufnachname"))
                            {
                                if (employee != null) employee.CallSignLastName = CheckForNullValues(entity, 13);
                            }
                            if (entity.Contains("Firmen E-Mail"))
                            {
                                if (employee != null) employee.MailAdress = CheckForNullValues(entity, 15);
                            }
                            if (entity.Contains("Stellen-Nr"))
                            {
                                if (employee != null) employee.Title = CheckForNullValues(entity, 21);
                            }
                            if (entity.Contains("Pensum"))
                            {
                                if (employee != null) employee.Workload = CheckForNullValues(entity, 8);
                            }
                            if (entity.Contains("Geschäftsbereich"))
                            {
                                if (employee != null) employee.Company = CheckForNullValues(entity, 18);
                            }
                            if (entity.Contains("Abteilungsname"))
                            {
                                if (employee != null) employee.Department = CheckForNullValues(entity, 16);
                            }
                            if (entity.Contains("Standort"))
                            {
                                var entityString = "";
                                if (employee != null) entityString = CheckForNullValues(entity, 10);
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

            employeeList = Finalize.FinalizeEmployees(employeeList);
            return employeeList;
        }


        public string CheckForNullValues(string property, int length)
        {
            if (property.Length > length)
            {
                return property.Substring(length);
            }
            return null;
        }

        public string [] ExtractAdressFromEntity(string completeAdress)
        {
            return completeAdress.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
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

                            if (entity.Contains("erster Arbeitstag") && entity.Length <= 30)
                            {
                                DateTime FirstWorkingDay = DateTime.Parse(entity.Substring(19));
                                if (record != null) record.FirstWorkingDay = FirstWorkingDay;

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
