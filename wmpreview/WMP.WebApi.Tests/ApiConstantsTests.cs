using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMP.WebApi.Tests
{
    [TestClass]
    public class ApiConstantsTests
    {

        #region Setup / Teardown

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestCleanup]
        public void TestCleanup()
        {


        }

        #endregion

        [TestMethod]
        public void DefaultApiRouteName_Is_DefaultApi()
        {
            //Arrange
            const string expectedApiRouteName = "DefaultApi";
            //Act
            const string actual = ApiConstants.DefaultApiRouteName;
            
            //Assert
            Assert.AreEqual(expectedApiRouteName, actual);
        } 
    }
}