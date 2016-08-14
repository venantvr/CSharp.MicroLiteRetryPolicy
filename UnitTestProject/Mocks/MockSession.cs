using System.Collections.Generic;
using System.Data;
using MicroLite;

namespace UnitTestProject.Mocks
{
    public class MockSession : ISession
    {
        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Begins a transaction using <see cref="T:System.Data.IsolationLevel"/>.ReadCommitted.
        /// </summary>
        /// <returns>
        /// An <see cref="T:MicroLite.ITransaction"/> with the default isolation level of the connection.
        /// </returns>
        /// <remarks>
        /// It is a good idea to perform all insert/update/delete actions inside a transaction.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      // perform actions against ISession.
        ///                      // ...
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public ITransaction BeginTransaction()
        {
            return new MockTransaction();
        }

        /// <summary>
        /// Executes the specified SQL query and returns the matching objects in a list.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam><param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>
        /// The objects that match the query in a list.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified SqlQuery is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      var query = new SqlQuery("SELECT * FROM Invoices WHERE CustomerId = @p0", 1324);
        ///                      var invoices = session.Fetch&lt;Invoice&gt;(query);
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public IList<T> Fetch<T>(SqlQuery sqlQuery)
        {
            return null;
        }

        /// <summary>
        /// Pages the specified SQL query and returns an <see cref="T:MicroLite.PagedResult`1"/> containing the desired results.
        /// </summary>
        /// <typeparam name="T">The type of object the query relates to.</typeparam><param name="sqlQuery">The SQL query to page before executing.</param><param name="pagingOptions">The <see cref="T:MicroLite.PagingOptions"/>.</param>
        /// <returns>
        /// A <see cref="T:MicroLite.PagedResult`1"/> containing the desired results.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified SqlQuery is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      var query = new SqlQuery("SELECT * FROM Customers WHERE LastName = @p0", "Smith");
        ///                      var customers = session.Paged&lt;Customer&gt;(query, PagingOptions.ForPage(page: 1, resultsPerPage: 25));
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public PagedResult<T> Paged<T>(SqlQuery sqlQuery, PagingOptions pagingOptions)
        {
            return null;
        }

        /// <summary>
        /// Returns the instance of the specified type which corresponds to the row with the specified identifier
        ///              in the mapped table, or null if the identifier values does not exist in the table.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam><param name="identifier">The record identifier.</param>
        /// <returns>
        /// An instance of the specified type or null if no matching record exists.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified instance is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      var customer = session.Single&lt;Customer&gt;(17867);
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public T Single<T>(object identifier) where T : class, new()
        {
            return null;
        }

        /// <summary>
        /// Returns a single instance based upon the specified SQL query, or null if no result is returned.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam><param name="sqlQuery">The SQL query to execute.</param>
        /// <returns>
        /// An instance of the specified type or null if no matching record exists.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified instance is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the query.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      var query = new SqlQuery("SELECT * FROM Customers WHERE EmailAddress = @p0", "fred.flintstone@bedrock.com");
        ///                      // This overload is useful to retrieve a single object based upon a unique value which isn't its identifier.
        ///                      var customer = session.Single&lt;Customer&gt;(query);
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public T Single<T>(SqlQuery sqlQuery)
        {
            return default(T);
        }

        /// <summary>
        /// Gets the advanced session operations.
        /// </summary>
        public IAdvancedSession Advanced { get; private set; }

        /// <summary>
        /// Deletes the database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to delete from the database.</param>
        /// <returns>
        /// true if the object was deleted successfully; otherwise false.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified instance is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the delete command.</exception>
        /// <example>
        /// <code>
        /// bool deleted = false;
        ///              using (var session = sessionFactory.OpenSession())
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      try
        ///                      {
        ///                          deleted = session.Delete(customer);
        ///                          transaction.Commit();
        ///                      }
        ///                      catch
        ///                      {
        ///                          deleted = false;
        ///                          transaction.Rollback();
        ///                          // Log or throw the exception.
        ///                      }
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public bool Delete(object instance)
        {
            return false;
        }

        /// <summary>
        /// Inserts a new database record for the specified instance.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param><exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified instance is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the insert command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      session.Insert(customer);
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public void Insert(object instance)
        {
        }

        /// <summary>
        /// Updates the database record for the specified instance with the current property values.
        /// </summary>
        /// <param name="instance">The instance to persist the values for.</param>
        /// <returns>
        /// true if the object was updated successfully; otherwise false.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">Thrown if the session has been disposed.</exception><exception cref="T:System.ArgumentNullException">Thrown if the specified instance is null.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error executing the update command.</exception>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenSession())
        ///              {
        ///                  using (var transaction = session.BeginTransaction())
        ///                  {
        ///                      session.Update(customer);
        ///                      transaction.Commit();
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public bool Update(object instance)
        {
            return false;
        }

        /// <summary>
        /// Gets the advanced session operations.
        /// </summary>
        IAdvancedReadOnlySession IReadOnlySession.Advanced
        {
            get { return Advanced; }
        }

        /// <summary>
        /// Gets the current transaction or null if one has not been started.
        /// </summary>
        public ITransaction CurrentTransaction { get; private set; }

        /// <summary>
        /// Gets the operations which allow additional objects to be queried in a single database call.
        /// </summary>
        public IIncludeSession Include { get; private set; }

        /// <summary>
        /// Begins the transaction with the specified <see cref="T:System.Data.IsolationLevel"/>.
        /// </summary>
        /// <param name="isolationLevel">The isolation level to use for the transaction.</param>
        /// <returns>
        /// An <see cref="T:MicroLite.ITransaction"/> with the specified <see cref="T:System.Data.IsolationLevel"/>.
        /// </returns>
        /// <remarks>
        /// It is a good idea to perform all insert/update/delete actions inside a transaction.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (var session = sessionFactory.OpenReadOnlySession()) // or sessionFactory.OpenSession()
        ///              {
        ///                  // This overload allows us to specify a specific IsolationLevel.
        ///                  using (var transaction = session.BeginTransaction(IsolationLevel.ReadCommitted))
        ///                  {
        ///                      // perform actions against ISession.
        ///                      // ...
        ///                      try
        ///                      {
        ///                          transaction.Commit();
        ///                      }
        ///                      catch (Exception exception)
        ///                      {
        ///                          transaction.Rollback();
        ///                          // Log or throw the exception.
        ///                      }
        ///                  }
        ///              }
        /// </code>
        /// </example>
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return null;
        }
    }
}