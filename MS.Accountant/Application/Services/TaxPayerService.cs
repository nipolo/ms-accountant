using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (taxPayer != null)
            {
                return taxPayer;
            }

            taxPayer = new TaxPayer(fullName, ssn, dateOfBirth, grossIncome, charitySpent);

            var taxes = _taxService.CalculateTaxes(taxPayer.GrossIncome - taxPayer.TaxFreeCharitySpendings);

            taxPayer.Taxes.AddRange(taxes);

            _cacheService.SaveTaxPayer(taxPayer);

            return taxPayer;
        }
    }
}
