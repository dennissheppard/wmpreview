using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace WMP.EFDalKit.Tests
{
    [TestClass]
    public class RepositoryBaseOfTContextTEntityTest
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        #region Nested type: ConstructorTest

        [TestClass]
        public class ConstructorTest
        {
            [TestMethod]
            public void Constructor_NonNullArguments_IsNotNull()
            {
                //Arrange
                using (var mockUnitOfWork = MockRepository.GenerateMock<IUnitOfWork<DbContextBase>>())
                {
                    var mockDbSet = MockRepository.GenerateMock<IDbSet<TestEntityClass>>();

                    //Act
                    var result =
                            MockRepository.GeneratePartialMock<RepositoryBase<DbContextBase, TestEntityClass>>(
                                    mockUnitOfWork);

                    //Assert
                    Assert.IsNotNull(result);
                }
            }
        }

        #endregion
    }
}