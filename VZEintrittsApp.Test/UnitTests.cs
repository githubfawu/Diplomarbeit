using FakeItEasy;
using VZEintrittsApp.DataAccess.Contexts;
using VZEintrittsApp.Import.PDFReader;
using VZEintrittsApp.Model;
using VZEintrittsApp.Model.EmployeeEntity;
using VZEintrittsApp.Test.Model;

namespace VZEintrittsApp.Test
{
    [TestClass]
    public class UnitTests
    {
        private FinalizeEmployee finalizeEmployee;
        public UnitTests()
        {
            finalizeEmployee = new FinalizeEmployee();
        }

        [TestMethod]
        public void Test_FinalizeEmployee()
        {
            //Arrange
            var employee = MockEmployee.GetEmployee();

            //Act
            var result = finalizeEmployee.Finalize(employee);

            //Assert
            Assert.AreEqual("CallSignFirstName", result.FirstName);
            Assert.AreEqual("CallSignLastName", result.LastName);
            Assert.AreEqual("TestBusinessArea", result.Company);
            Assert.AreEqual(null, result.VzAcademicTitle);
            Assert.AreEqual($"http://mysite.vz.ch/Person.aspx?accountname=ZH01%5C{result.Abbreviation}", result.HomePage);
        }

        [TestMethod]
        public void Test_RepositoryWithFakeItEasy()
        {
            //Arrange
            var employee = A.Fake<Employee>();
            var repository = A.Fake<IRepository>();
            A.CallTo(() => repository.ReadAllAdAttributes(A.Dummy<string>()));

            //Act
            employee = repository.ReadAllAdAttributes(A.Dummy<string>());

            //Assert
            A.CallTo(() => repository.ReadAllAdAttributes(A.Dummy<string>())).MustHaveHappened();
        }
    }
}