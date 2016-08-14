using MicroLite;

namespace UnitTestProject.Mocks
{
    public class MockTransaction : ITransaction
    {
        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Commits the transaction, applying all changes made within the transaction scope.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the transaction has been completed.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error committing the transaction.</exception>
        public void Commit()
        {
        }

        /// <summary>
        /// Rollbacks the transaction, undoing all changes made within the transaction scope.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the transaction has been completed.</exception><exception cref="T:MicroLite.MicroLiteException">Thrown if there is an error rolling back the transaction.</exception>
        public void Rollback()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this transaction is active.
        /// </summary>
        public bool IsActive { get; private set; }
    }
}