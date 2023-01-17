using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class CacheService<T> : ICacheService<T> where T : IEntity
    {
        private static readonly ConcurrentDictionary<long, T> _instancesCache = new();
        private static readonly ConcurrentDictionary<long, SemaphoreSlim> _instancesMutexes = new();
        private static readonly ReaderWriterLockSlim _getInstanceMutex = new();

        public async Task<AcquiredCacheInstance<T>> AcquireCacheInstanceAsync(long id)
        {
            _getInstanceMutex.EnterReadLock();

            if (!_instancesMutexes.TryGetValue(id, out var mutex))
            {
                mutex = new SemaphoreSlim(1, 1);
                _instancesMutexes.TryAdd(id, mutex);
            }

            _getInstanceMutex.ExitReadLock();

            await mutex.WaitAsync();

            _instancesCache.TryGetValue(id, out var instance);

            return new AcquiredCacheInstance<T>(instance, mutex);
        }

        public bool AddOrUpdate(T instance, SemaphoreSlim mutex)
        {
            _instancesMutexes.TryGetValue(instance.Id, out var mutexFromCache);

            if (mutex.CurrentCount > 0 || mutex != mutexFromCache)
            {
                return false;
            }

            if (!_instancesCache.ContainsKey(instance.Id))
            {
                return _instancesCache.TryAdd(instance.Id, instance);
            }

            _instancesCache[instance.Id] = instance;

            return true;
        }
    }
}
