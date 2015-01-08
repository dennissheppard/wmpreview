using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace WMP.EFDalKit.Tests
{
    /// <summary>
    ///This is a test class for UnitOfWorkTest and is intended
    ///to contain all UnitOfWorkTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UnitOfWorkTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
        }

        /// <summary>
        ///A test for UnitOfWork Constructor
        ///</summary>
        [TestMethod]
        public void UnitOfWorkConstructor_IsNotNull()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();


            //Act
            var actual = new UnitOfWork<DbContext>(dalMocks.MockDbContextFactory);

            //Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void Commit_NoObjectsInContext_CallsDbContextSaveChangesAndReturnsZero()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();
            dalMocks.MockDbContextFactory.Expect(dbContextFactory => dbContextFactory.Create())
                    .Return(dalMocks.MockDbContext);

            UnitOfWork<DbContext> target = new UnitOfWork<DbContext>(dalMocks.MockDbContextFactory);
            int expected = 0;
            dalMocks.MockDbContext.Expect(x => x.SaveChanges()).Return(expected);
            int actual;

            //Act
            actual = target.SaveChanges();

            //Assert
            Assert.AreEqual(expected, actual);
            dalMocks.MockDbContext.VerifyAllExpectations();
        }

        [TestMethod()]
        public void Dispose_DbContextDispose_IsCalled()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();
            dalMocks.MockDbContext.Stub(x => x.Dispose());
            dalMocks.MockDbContextFactory.Expect(x => x.Create()).Return(dalMocks.MockDbContext);
            UnitOfWork<DbContext> target = new UnitOfWork<DbContext>(dalMocks.MockDbContextFactory);

            //Act
            var ctxt = target.DbContext;
            target.Dispose();

            //Assert
            dalMocks.MockDbContext.AssertWasCalled(x => x.Dispose());
        }

        [TestMethod()]
        [ExpectedException(typeof (ObjectDisposedException))]
        public void DbContext_AfterDispose_ObjectDisposedExceptionThrown()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();
            dalMocks.MockDbContext.Stub(x => x.Dispose());
            dalMocks.MockDbContextFactory.Expect(x => x.Create()).Return(dalMocks.MockDbContext);
            UnitOfWork<DbContext> target = new UnitOfWork<DbContext>(dalMocks.MockDbContextFactory);

            //Act
            target.Dispose();
            var actual = target.DbContext;

            //Assert
            Assert.Fail("Expected exception not thrown");
        }

        [TestMethod]
        public void DbContext_OnFirstAccess_CallsFactoryCreate()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();

            dalMocks.MockDbContextFactory.Expect(x => x.Create()).Return(dalMocks.MockDbContext);
            var target = new UnitOfWork<DbContext>(dalMocks.MockDbContextFactory);

            //Act
            var actual = target.DbContext;

            //Assert
            Assert.IsNotNull(actual);
            dalMocks.MockDbContextFactory.VerifyAllExpectations();
        }


        [TestMethod]
        [ExpectedException(typeof (DbEntityValidationException))]
        public void Commit_InvalidProperty_ExpectValidationException()
        {
            //Arrange
            var dalMocks = new DalFrameworkMocks();
            var testDbContext = new DalFrameworkTestUtility.TestDbContext();
            dalMocks.MockTestDbContextFactory.Expect(x => x.Create()).Return(testDbContext);
            using (var target = new UnitOfWork<DalFrameworkTestUtility.TestDbContext>(dalMocks.MockTestDbContextFactory)
                    )
            {
                var entity = testDbContext.TestValidationEntities.Create();
                testDbContext.TestValidationEntities.Add(entity);
                entity.ValidatedName = null;

                //Act
                target.SaveChanges();
            }

            //Assert
            Assert.Fail("No validation exception has been thrown");
        }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class


        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        #region Helper methods

        private static IDbContextFactory<DbContext> GetMockDbContextFactory(DalFrameworkMocks dalMocks)
        {
            IDbContextFactory<DbContext> contextFactory = dalMocks.MockDbContextFactory;
            dalMocks.MockDbContextFactory.Expect(dbContextFactory => dbContextFactory.Create())
                    .Return(dalMocks.MockDbContext);
            return contextFactory;
        }

        #endregion
    }
}