using System.Collections.Generic;
using Contracts;
using MicroLite;
using RetryPolicies.Exceptions;
using RetryPolicies.Interfaces;

namespace UnitTestProject
{
    public class TestRepository<T> : IRepository<T> where T : new()
    {
        // ReSharper disable once StaticMemberInGenericType
        private static int _occurence;
        // Tableau de booléens : "false" signifie qu'une erreur de connexion s'est produite, "true" que tout s'est bien passé...
        private readonly bool[] _sequence =
        {
            false, false, true, false, true, true, true, true, true, false, false, false, false, false, true, true, true, true, true, true
        };

        // ReSharper disable once UnusedParameter.Local
        public TestRepository(ISession session)
        {
        }

        public IEnumerable<T> GetPaged(int page, int take)
        {
            _occurence = _occurence + 1;

            if (!_sequence[(_occurence - 1) % _sequence.Length])
            {
                ThrowFakeException();
            }

            for (var i = 0; i < take; i++)
            {
                yield return new T();
            }
        }

        private static void ThrowFakeException()
        {
            var exception = SqlExceptionHelper.Generate(SqlExceptionNumber.TransportLevelReceiving);
            throw new MicroLiteException("Transient Fault", exception);
        }
    }
}