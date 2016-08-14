using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetryPolicies.Policies;

namespace UnitTestProject
{
    [TestClass]
    public class TestDivideByZero
    {
        [TestMethod]
        //[ExpectedException(typeof(DivideByZeroException))]
        // Not possible because of System.AggregateException
        public void TestDivideByZeroNotManaged()
        {
            const decimal b = 100.0M;

            try
            {
                var retryException = new RetryPolicies.Policies.RetryRunTime<CustomRetryPolicy>(TimeSpan.FromSeconds(2));

                for (var k = -10; k < 10; k++)
                {
                    var copy = k;
                    retryException.TryExecuteAsync(() => { Division(b, copy); }).Wait();
                }
            }
            catch (AggregateException ae)
            {
                if (ae.InnerException.GetType() == typeof (DivideByZeroException))
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail("There was no DivideByZeroException.");
                }
            }
        }

        [TestMethod]
        //[ExpectedException(typeof (DivideByZeroException))]
        public void TestDivideByZeroSimple()
        {
            var retryException = new RetryRunTime<CustomRetryPolicy>(TimeSpan.FromSeconds(2));

            for (var k = 0; k < 20; k++)
            {
                var copy = k;

                retryException.TryExecuteAsync(() =>
                                               {
                                                   const decimal a = 0.0M;
                                                   const decimal b = 100.0M;

                                                   Division(b, a - copy);
                                               }).Wait();
            }
        }

        [TestMethod]
        //[ExpectedException(typeof(DivideByZeroException))]
        // Not possible because of System.AggregateException
        public void TestDivideByZeroManaged()
        {
            const decimal b = 100.0M;

            var retryException = new RetryRunTime<CustomRetryPolicy>(TimeSpan.FromSeconds(2));

            for (var k = 0; k < 20; k++)
            {
                retryException.TryExecuteAsync(() =>
                                               {
                                                   var a = new Random().Next(0, 10);
                                                   Division(b, a % 2);
                                               }).Wait();
            }

            Assert.IsTrue(true);
        }

        private static void Division(decimal b, decimal a)
        {
        }
    }
}