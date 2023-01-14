using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Module;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class TaxService : ITaxService
    {
        private readonly List<ITax> _taxes = new();
        private readonly decimal _maxCharityFreePercent;
        public TaxService(IOptions<TaxesSettings> taxesSettings)
        {
            _taxes.Add(new IncomeTax(taxesSettings.Value.Taxes[nameof(IncomeTax)]));
            _taxes.Add(new SocialContributionsTax(taxesSettings.Value.Taxes[nameof(SocialContributionsTax)]));
            _maxCharityFreePercent = taxesSettings.Value.MaxCharityFreePercent;
        }

        public (List<TaxInstance> Taxes, decimal TaxFreeCharitySpendings) CalculateTaxes(decimal startingAmount, decimal charitySpent)
        {
            var taxFreeCharitySpendings = Math.Min(charitySpent, _maxCharityFreePercent / 100.0m * startingAmount);

            var taxes = _taxes.Select(x => new TaxInstance
            {
                TaxId = x.Id,
                TaxAmount = x.CalculateTax(startingAmount - taxFreeCharitySpendings)
            }).ToList();

            return (taxes, taxFreeCharitySpendings);
        }

        public ITax FindTaxByName(string taxName)
        {
            return _taxes.FirstOrDefault(x => x.Name == taxName);
        }
    }
}
