using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace WMP.EFDalKit.Tests
{
    [TestClass]
    public class RepositoryBaseOfTContextTest
    {
        #region Nested type: RepositoryBaseConstructorTest

        [TestClass]
        public class RepositoryBaseConstructorTest
        {
            [TestMethod]
            public void Constructor_WithIDataContext_IsNotNull()
            {
                //Arrange
                var mockUnitOfWork = MockRepository.GenerateMock<IUnitOfWork<DbContextBase>>();
                //Act
                var target = MockRepository.GeneratePartialMock<RepositoryBase<DbContextBase>>(mockUnitOfWork);
                //var target = <RepositoryBase<DalFrameworkTestUtility.TestDbContext, TestEntityClass>>(mockUnitOfWork);
                //Assert
                Assert.IsNotNull(target);
            }


            [TestMethod]
            [ExpectedException(typeof (ArgumentNullException))]
            public void Constructor_NullIDataContext_ThrowsArgumentNullException()
            {
                //Arrange               

                //Act
                var target = new DalFrameworkTestUtility.MockRepositoryBase(null);

                //Assert
                Assert.Fail("No exception thrown");
            }
        }

        #endregion
    }
}