using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WMP.EFDalKit.Tests
{
    public class DalFrameworkTestUtility
    {
        //internal static string ConnectionString = @"Server=.\SQLEXPRESS;Database=DalFrameworkTest;Trusted_Connection=True;";

        #region Nested type: MockRepositoryBase

        public class MockRepositoryBase : RepositoryBase<TestDbContext>
        {
            public MockRepositoryBase(IUnitOfWork<TestDbContext> UoW) : base(UoW)
            {
            }
        }

        #endregion

        #region Nested type: TestDbContext

        public class TestDbContext : DbContextBase
        {
            public TestDbContext()
                    : base()
            {
            }

            public TestDbContext(string nameOrConnectionString)
                    : base(nameOrConnectionString)
            {
            }

            public DbSet<TestEntityClass> TestEntities { get; set; }
            public DbSet<TestValidationEntityClass> TestValidationEntities { get; set; }
        }

        #endregion
    }

    /// <summary>
    /// Mock Entity Class to be used for testing purposes
    /// </summary>
    public class TestEntityClass
    {
        public virtual int TestEntityClassId { get; set; }
        public virtual string TestEntityClassName { get; set; }
    }

    /// <summary>
    /// Mock Entity Class to be used for testing purposes
    /// </summary>
    public class TestValidationEntityClass
    {
        public virtual int TestValidationEntityClassId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string ValidatedName { get; set; }
    }
}