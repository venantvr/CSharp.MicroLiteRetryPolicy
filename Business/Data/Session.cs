using System;
using MicroLite.Configuration;

namespace Business.Data
{
    public class Session : IDisposable
    {
        public Session()
        {
            var sessionFactory = Configure.Fluently().ForMsSql2012Connection("BreakAway").CreateSessionFactory();
            Configure.Extensions().WithAttributeBasedMapping();
            var session = sessionFactory.OpenSession();
            var transaction = session.BeginTransaction();
        }

        public void Dispose()
        {
        }
    }
}