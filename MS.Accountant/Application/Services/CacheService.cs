using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services
{
    public class CacheService : ICacheService
    {
        private static readonly ConcurrentDictionary<long, TaxPayer> _taxPayersCache = new();

        public TaxPayer GetTaxPayer(long ssn)
        {
            _taxPayersCache.TryGetValue(ssn, out var taxPlayer);

            return taxPlayer;
        }

        public bool SaveTaxPayer(TaxPayer taxPayer)
        {
            return _taxPayersCache.TryAdd(taxPayer.SSN, taxPayer);
        }
    }
}
