using System;
using System.Linq;
using Business.Data;
using MicroLite;
using MicroLite.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetryPolicies.Exceptions;
using RetryPolicies.Policies;

namespace UnitTestProject
{
    [TestClass]
    public class TestMicroLite
    {
        private ISessionFactory _sessionFactory;
        private Mock<ITransaction> _mockITransaction;

        [TestInitialize]
        public void Prepare()
        {
            //_sessionFactory = new MockSessionFactory();

            var mockSessionFactory = new Mock<ISessionFactory>();
            var mockISession = new Mock<ISession>();
            _mockITransaction = new Mock<ITransaction>();

            mockISession.Setup(foo => foo.BeginTransaction()).Returns(_mockITransaction.Object);
            mockSessionFactory.Setup(foo => foo.OpenSession()).Returns(mockISession.Object);
            _mockITransaction.Setup(foo => foo.Commit());
            _mockITransaction.Setup(foo => foo.Rollback());

            _sessionFactory = mockSessionFactory.Object;
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        [TestMethod]
        public void TestMicroliteExceptionGreen()
        {
            var retry = new RetryPolicies.Policies.RetryRunTime<MicroLiteRetryPolicy>(() => new MicroLiteRetryPolicy(10), TimeSpan.FromMilliseconds(10));

            using (var session = _sessionFactory.OpenSession())
            {
                var customersRepository = new TestRepository<Customers>(session);

                for (int j = 0; j < 20; j++)
                {
                    retry.TryExecuteAsync(() =>
                                          {
                                              using (var transaction = session.BeginTransaction())
                                              {
                                                  var customers = customersRepository.GetPaged(j, 1000).ToList();
                                                  transaction.Commit();
                                              }
                                          }).Wait();
                }
            }
        }

        [TestMethod]
        public void TestMicroliteExceptionRed()
        {
            // 2 tentatives espacées de 10 millisecondes...
            var retry = new RetryRunTime<MicroLiteRetryPolicy>(() => new MicroLiteRetryPolicy(2), TimeSpan.FromMilliseconds(10));
            Exception expectedException = null;

            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    var customersRepository = new TestRepository<Customers>(session);

                    for (int j = 0; j < 20; j++)
                    {
                        retry.TryExecuteAsync(() =>
                                              {
                                                  using (var transaction = session.BeginTransaction())
                                                  {
                                                      var customers = customersRepository.GetPaged(j, 1000).ToList();
                                                      transaction.Commit();
                                                  }
                                              }).Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            _mockITransaction.Verify(foo => foo.Commit(), Times.Exactly(6));

            Assert.IsInstanceOfType(expectedException.InnerException, typeof (MaxCountExpiredException));
        }
    }
}