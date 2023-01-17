using System;
using System.Threading.Tasks;

using MS.Accountant.Application.Services.Abstractions;
using MS.Accountant.Domain.Entities;
using MS.Accountant.Domain.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class TaxPayerService : ITaxPayerService
    {
        private readonly ITaxService _taxService;
        private readonly ICacheService<TaxPayer> _taxPlayerCacheService;

        public TaxPayerService(
            ITaxService taxService,
            ICacheService<TaxPayer> taxPlayerCacheService)
        {
            _taxService = taxService;
            _taxPlayerCacheService = taxPlayerCacheService;
        }

        public async Task<TaxPayer> CreateTaxPayerAsync(
            string fullName,
            long ssn,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent)
        {
            using var acquiredCacheInstance = await _taxPlayerCacheService.AcquireCacheInstanceAsync(ssn);

            var taxPayer = acquiredCacheInstance.Instance;
            if (taxPayer != null
                && taxPayer.Equals(fullName, dateOfBirth, grossIncome, charitySpent))
            {
                return taxPayer;
            }

            var (taxes, taxFreeCharitySpendings) = _taxService.CalculateTaxes(grossIncome, charitySpent);

            taxPayer = new TaxPayer(fullName, ssn, dateOfBirth, grossIncome, charitySpent, taxes, taxFreeCharitySpendings);

            _taxPlayerCacheService.AddOrUpdate(taxPayer, acquiredCacheInstance.Mutex);

            return taxPayer;
        }
    }
}
