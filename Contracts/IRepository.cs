using System.Collections.Generic;

namespace Contracts
{
    public interface IRepository<out T>
    {
        IEnumerable<T> GetPaged(int skip, int take);
    }
}