using MicroLite;

namespace UnitTestProject.Mocks
{
    public class MockTransaction : ITransaction
    {
        /// <summary>
        /// Ex�cute les t�ches d�finies par l'application associ�es � la lib�ration ou � la red�finition des ressources non manag�es.
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