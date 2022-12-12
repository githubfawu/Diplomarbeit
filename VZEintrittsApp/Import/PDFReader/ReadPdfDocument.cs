using System.Collections.Generic;
using System.Text.RegularExpressions;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import.PDFReader
{
    internal class ReadPdfDocument : IReadDocument
    {
        public List<Employee> ProcessDocument(string file)
        {
            List<Employee> employeeList = new List<Employee>();

            using (var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions()))
            {
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using (var pageReader = docReader.GetPageReader(i))
                    {
                        Employee? user = null;

                        var text = pageReader.GetText();
                        var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);

                        foreach (var entity in splitString)
                        {
                            if (entity.Contains("Mitarb.-Nr.:"))
                            {
                                var employeeNrString = entity.Substring(13);
                                var emplyeeNr = int.Parse(employeeNrString);

                                var newEmployee = new Employee()
                                {
                                    EmployeeNr = emplyeeNr
                                };
                                employeeList.Add(newEmployee);
                                user = newEmployee;
                            }

                            if (entity.Contains("Kürzel"))
                            {
                                var maAbbreviation = entity.Substring(8);
                                if (user != null) user.Abbreviation = maAbbreviation;
                            }

                        }
                    }
                }
            }
            return employeeList;
        }
    }
}
