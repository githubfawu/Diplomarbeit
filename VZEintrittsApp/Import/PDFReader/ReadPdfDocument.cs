using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.Domain;

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
                                if (employee != null) employee.Abbreviation = entity.Substring(8);
                            }

                            if (entity.Contains("Vorname"))
                            {
                                if (employee != null) employee.Name = entity.Substring(9);
                            }

                            if (entity.Contains("Name"))
                            {
                                if (employee != null) employee.LastName = entity.Substring(6);
                            }

                            if (entity.Contains("Stellen-Nr"))
                            {
                                if (employee != null) employee.Title = entity.Substring(21);
                            }

                            if (entity.Contains("Pensum"))
                            {
                                if (employee != null) employee.Workload = entity.Substring(8);
                            }

                            if (entity.Contains("Geschäftsbereich"))
                            {
                                if (employee != null) employee.Company = entity.Substring(18);
                            }

                        }
                    }
                }
            }
            return employeeList;
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
                                if (record != null) record.Abbreviation = entity.Substring(8);
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
