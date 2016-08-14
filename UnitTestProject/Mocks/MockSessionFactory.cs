using MicroLite;
using MicroLite.Driver;

namespace UnitTestProject.Mocks
{
    public class MockSessionFactory : ISessionFactory
    {
        public IAsyncReadOnlySession OpenAsyncReadOnlySession()
        {
            return null;
        }

        public IAsyncReadOnlySession OpenAsyncReadOnlySession(ConnectionScope connectionScope)
        {
            return null;
        }

        public IAsyncSession OpenAsyncSession()
        {
            return null;
        }

        public IAsyncSession OpenAsyncSession(ConnectionScope connectionScope)
        {
            return null;
        }

        public IReadOnlySession OpenReadOnlySession()
        {
            return null;
        }

        public IReadOnlySession OpenReadOnlySession(ConnectionScope connectionScope)
        {
            return null;
        }

        public ISession OpenSession()
        {
            return new MockSession();
        }

        public ISession OpenSession(ConnectionScope connectionScope)
        {
            return null;
        }

        public string ConnectionName { get; private set; }
        public IDbDriver DbDriver { get; private set; }
    }
}