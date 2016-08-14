using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.Business;
using MicroLite;
using MicroLite.Configuration;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using RetryPolicies.Policies;

namespace MicroLitePoc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var retry = new RetryRunTime<RetryPolicies.Policies.CustomRetryPolicy>(TimeSpan.FromSeconds(2));

            var retryException = new RetryRunTime<RetryPolicies.Policies.CustomRetryPolicy>(TimeSpan.FromSeconds(2) /*, typeof(DivideByZeroException), typeof(MicroLiteException), typeof(SqlException)*/);

            //Console.ReadKey();

            var a = 10.0M;

            //for (var k = 0; k < 20; k++)
            //    retryException.TryExecuteAsync(() =>
            //                                   {
            //                                       //const decimal a = 0.0M;
            //                                       const decimal b = 100.0M;

            //                                       var i = Division(b, a - k);
            //                                   }).Wait();

            var autoEvent = new AutoResetEvent(false);

            var sessionFactory = Configure.Fluently().ForMsSql2012Connection("BreakAway").CreateSessionFactory();
            Configure.Extensions().WithAttributeBasedMapping();

            var evt = new ManualResetEvent(false);

            // Step 1
            Task.Run(() =>
                     {
                         while (!evt.WaitOne(200))
                         {
                             using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BreakAway"].ConnectionString))
                             {
                                 var database = connection.Database;

                                 var queryString = string.Format(@"ALTER DATABASE [{0}] SET OFFLINE WITH ROLLBACK IMMEDIATE ALTER DATABASE [{0}] SET ONLINE", database);

                                 var command = new SqlCommand(queryString, connection);

                                 connection.Open();
                                 command.ExecuteNonQuery();
                                 connection.Close();
                             }
                         }
                         // ReSharper disable once FunctionNeverReturns
                     });

            using (var session = sessionFactory.OpenSession())
            {
                //var customersRepository = new Repository<Customers>(session);

                retry.TryExecuteAsync(() =>
                                      {
                                          // ReSharper disable once AccessToDisposedClosure
                                          using (var transaction = session.BeginTransaction())
                                          {
                                              Console.WriteLine(@"Trying...");

                                              Test_03(session, transaction);
                                              //var customers = customersRepository.GetPaged(1, 1000);

                                              transaction.Commit();
                                          }
                                      }).Wait();
            }

            var retryStrategy = new Incremental(100, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));

            // Step 2 
            var retryPolicy = new RetryPolicy<CustomDatabaseTransientErrorDetectionStrategy>(retryStrategy);

            retryPolicy.ExecuteAction(() =>
                                      {
                                          using (var session = sessionFactory.OpenSession())
                                          {
                                              using (var transaction = session.BeginTransaction())
                                              {
                                                  Console.WriteLine(@"Trying...");

                                                  Test_03(session, transaction);
                                                  transaction.Commit();
                                              }
                                          }
                                      });

            Console.WriteLine(@"Ok");
            Console.ReadKey();

            //using (var session = sessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        Test_03(session, transaction);
            //        transaction.Commit();
            //    }
            //}
        }

        private static decimal Division(decimal b, decimal a)
        {
            return b / a;
        }

        private static void Test_03(ISession session, ITransaction transaction)
        {
            const string query =
                @"SELECT TOP 100000 [ContactID]
                              ,[CustomerTypeID]
                              ,[InitialDate]
                              ,[PrimaryDesintation]
                              ,[SecondaryDestination]
                              ,[PrimaryActivity]
                              ,[SecondaryActivity]
                              ,[Notes]
                              ,[RowVersion]
                          FROM [BreakAway].[dbo].[Customers]";

            //try
            //{
            //throw new SqlException
            //      {

            //      }

            var customers = session.Fetch<dynamic>(new SqlQuery(query));
            var data = customers.Select(o => new ReadOnlyCustomers_V2(o));
            var first = data.First();
            //first.ModifyNotes(@"Something strange happened...");

            //var sqlQuery = new SqlQuery(@"update Customers set Notes=@1 where ContactID=@2", new SqlArgument(first.Notes, DbType.AnsiString), new SqlArgument(first.ContactId, DbType.Int32));
            //var i = session.Advanced.Execute(sqlQuery);

            //var objectDelta = new ObjectDelta(typeof(Customers), first);
            //objectDelta.AddChange(@"Notes", first.Notes);
        }

        //                @"SELECT TOP 100000 [ContactID]
        //    const string query = 

        //private static void Test_01(ISession session, ITransaction transaction)

        //    IList<Customers> customers;
        //                      ,[CustomerTypeID]
        //                      ,[InitialDate]
        //                      ,[PrimaryDesintation]
        //                      ,[SecondaryDestination]
        //                      ,[PrimaryActivity]
        //                      ,[SecondaryActivity]
        //                      ,[Notes]
        //                      ,[RowVersion]
        //                  FROM [BreakAway].[dbo].[Customers]";

        //    try
        //    {
        //        customers = session.Fetch<Customers>(new SqlQuery(query));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //private static void Test_02(ISession session, ITransaction transaction)
        //{
        //    IList<dynamic> customers;
        //    IEnumerable<ReadOnlyCustomers_V1> data;

        //    const string query = 
        //                    @"SELECT TOP 100000 [ContactID]
        //                          ,[CustomerTypeID]
        //                          ,[InitialDate]
        //                          ,[PrimaryDesintation]
        //                          ,[SecondaryDestination]
        //                          ,[PrimaryActivity]
        //                          ,[SecondaryActivity]
        //                          ,[Notes]
        //                          ,[RowVersion]
        //                      FROM [BreakAway].[dbo].[Customers]";

        //    try
        //    {
        //        customers = session.Fetch<dynamic>(new SqlQuery(query));
        //        data = customers.Select(o => new ReadOnlyCustomers_V1(o.ContactID, o.CustomerTypeID, o.InitialDate, o.PrimaryDesintation, o.SecondaryDestination, o.PrimaryActivity, o.SecondaryActivity, o.Notes));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}