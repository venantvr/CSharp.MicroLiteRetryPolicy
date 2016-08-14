using System.Collections.Generic;
using Contracts;
using MicroLite;
using MicroLite.Builder;

namespace Business.Data
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<T> GetPaged(int page, int take)
        {
            var query = SqlBuilder
                .Select("*")
                .From(typeof (T))
                .ToSqlQuery();

            var result = _session.Paged<T>(query, PagingOptions.ForPage(page, take)).Results;

            return result;
        }
    }
}