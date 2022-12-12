using System.Collections.Generic;
using System.Text.RegularExpressions;
using Docnet.Core;
using Docnet.Core.Models;
using VZEintrittsApp.Domain;

namespace VZEintrittsApp.Import.PDFReader
{
    internal class ReadPdfDocument : IReadDocument
    {
        public List<User> ProcessDocument(string file)
        {
            List<User> employeeList = new List<User>();

            using (var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions()))
            {
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using (var pageReader = docReader.GetPageReader(i))
                    {
                        User? user = null;

                        var text = pageReader.GetText();
                        var splitString = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);

                        foreach (var entity in splitString)
                        {
                            if (entity.Contains("Mitarb.-Nr.:"))
                            {
                                var employeeNrString = entity.Substring(13);
                                var emplyeeNr = int.Parse(employeeNrString);

                                var neuerMitarbeiter = new User()
                                {
                                    EmployeeNr = emplyeeNr
                                };
                                employeeList.Add(neuerMitarbeiter);
                                user = neuerMitarbeiter;
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
