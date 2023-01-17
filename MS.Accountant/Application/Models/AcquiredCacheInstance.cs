using System;
using System.Threading;

namespace MS.Accountant.Application.Models
{
    public class AcquiredCacheInstance<T> : IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;
        private readonly T _instance;
        private readonly SemaphoreSlim _mutex;

        public AcquiredCacheInstance(T instance, SemaphoreSlim mutex)
        {
            _instance = instance;
            _mutex = mutex;
        }

        ~AcquiredCacheInstance() => Dispose(false);

        public T Instance => _instance;

        public SemaphoreSlim Mutex => _mutex;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _mutex.Release();
                }

                _disposedValue = true;
            }
        }
    }
}
