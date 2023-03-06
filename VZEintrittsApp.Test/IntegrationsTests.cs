using VZEintrittsApp.DataAccess.Contexts;
using VZEintrittsApp.Import.PDFReader;
using VZEintrittsApp.Import;
using VZEintrittsApp.Test.ImportFile;
using VZEintrittsApp.Test.Model;

namespace VZEintrittsApp.Test
{
    [TestClass]
    public class IntegrationsTests
    {
        private IReadDocument documentReader;
        private DbContext dbContext;
        private RecordContext recordContext;
        private API.AD.DirectoryServices directoryServices;

        public IntegrationsTests()
        {
            documentReader = new ReadPdfDocument();
            dbContext = new DbContext();
            recordContext = new RecordContext(dbContext);
            directoryServices = new API.AD.DirectoryServices(new LoggerContext(dbContext), new AttributeNotationContext(dbContext));
        }

        [TestMethod]
        public void Test_ReadPDF_AbbreviationFromFileIsNotNull()
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

        [TestMethod]
        public void Test_RecordingContext_WriteNewRecord()
        {
            //Arrange
            var record = MockRecord.GetRecord();
            
            //Act
            recordContext.SaveNewRecord(record);
            var result = recordContext.RecordExists(record);
            recordContext.DeleteRecord(record);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_CreateUserInActiveDirectory()
        {
            //Arrange
            var employee = MockEmployee.GetEmployee();

            //Act
            directoryServices.CreateNewAdAccount(employee);
            var result = directoryServices.CheckIfUserExists(employee.Abbreviation);
            directoryServices.DeleteAdAccount(employee.Abbreviation);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
