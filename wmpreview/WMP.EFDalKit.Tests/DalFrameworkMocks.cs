using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Rhino.Mocks;

namespace WMP.EFDalKit.Tests
{
    public class DalFrameworkMocks
    {
        private DbContext _MockDbContext;
        private IDbContextFactory<DbContext> _MockDbContextFactory;
        private IDbContextFactory<DalFrameworkTestUtility.TestDbContext> _MockTestDbContextFactory;

        public DalFrameworkMocks()
        {
            InitializeMocks();
        }

        public DbContext MockDbContext
        {
            get { return _MockDbContext; }
            protected set { _MockDbContext = value; }
        }

        public IDbContextFactory<DbContext> MockDbContextFactory
        {
            get { return _MockDbContextFactory; }
            protected set { _MockDbContextFactory = value; }
        }

        public IDbContextFactory<DalFrameworkTestUtility.TestDbContext> MockTestDbContextFactory
        {
            get { return _MockTestDbContextFactory; }
            protected set { _MockTestDbContextFactory = value; }
        }

        private void InitializeMocks()
        {
            _MockDbContextFactory = MockRepository.GenerateMock<IDbContextFactory<DbContext>>();
            _MockTestDbContextFactory =
                    MockRepository.GenerateMock<IDbContextFactory<DalFrameworkTestUtility.TestDbContext>>();

            _MockDbContext = MockRepository.GenerateMock<DbContext>();
        }
    }
}