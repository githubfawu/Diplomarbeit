using VZEintrittsApp.Import.PDFReader;
using VZEintrittsApp.Import;
using VZEintrittsApp.Test.ImportFile;

namespace VZEintrittsApp.Test
{
    [TestClass]
    public class IntegrationsTests
    {
        private IReadDocument documentReader;

        public IntegrationsTests()
        {
            documentReader = new ReadPdfDocument();
        }

        [TestMethod]
        public void Test_ReadPDF_GetAbbreviationFromFile()
        {
            //Arrange
            var bytes = Convert.FromBase64String(FileData.GetFile());
            string path = Path.GetTempPath() + "Test.pdf";
            File.WriteAllBytes(path, bytes);

            var listOfEmployees = documentReader.ReadUsers(path);

            //Act
            var result = listOfEmployees[0].Abbreviation;
            
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
