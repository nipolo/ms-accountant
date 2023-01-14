using System;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class TaxPayerService : ITaxPayerService
    {
        private readonly ITaxService _taxService;
        private readonly ICacheService _cacheService;

        public TaxPayerService(
            ITaxService taxService,
            ICacheService cacheService)
        {
            _taxService = taxService;
            _cacheService = cacheService;
        }

        public TaxPayer CreateTaxPayer(
            string fullName,
            long ssn,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent)
        {
            var taxPayer = _cacheService.GetTaxPayer(ssn);
            if (taxPayer != null
                && taxPayer.FullName == fullName
                && taxPayer.DateOfBirth == dateOfBirth
                && taxPayer.GrossIncome == grossIncome
                && taxPayer.CharitySpent == charitySpent)
            {
                return taxPayer;
            }

            taxPayer = new TaxPayer(fullName, ssn, dateOfBirth, grossIncome, charitySpent);

            var (taxes, taxFreeCharitySpendings) = _taxService.CalculateTaxes(taxPayer.GrossIncome,
                charitySpent);

            taxPayer.Taxes.AddRange(taxes);
            taxPayer.TaxFreeCharitySpendings = taxFreeCharitySpendings;

            _cacheService.SaveTaxPayer(taxPayer);

            return taxPayer;
        }
    }
}
