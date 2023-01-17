using System.Threading;
using System.Threading.Tasks;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ICacheService<T> where T : IEntity
    {
        Task<AcquiredCacheInstance<T>> AcquireCacheInstanceAsync(long id);

        bool AddOrUpdate(T instance, SemaphoreSlim mutex);
    }
}