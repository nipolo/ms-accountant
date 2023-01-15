using System;
using System.Collections.Generic;
using System.Linq;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class TaxService : ITaxService
    {
        private readonly Dictionary<string, ITax> _taxes = new();
        private readonly decimal _maxCharityFreePercent;
        private readonly ITaxSettingsService _taxSettingsService;

        public TaxService(ITaxSettingsService taxSettingsService)
        {
            _taxSettingsService = taxSettingsService;
            _maxCharityFreePercent = _taxSettingsService.GetMaxCharityFreePercent();

            AddTax<IncomeTax>();
            AddTax<SocialContributionsTax>();
        }

        public (Dictionary<string, TaxInstance> Taxes, decimal TaxFreeCharitySpendings) CalculateTaxes(decimal startingAmount, decimal charitySpent)
        {
            var taxFreeCharitySpendings = Math.Min(charitySpent, _maxCharityFreePercent / 100.0m * startingAmount);

            var taxes = _taxes.Values.Select(x => new TaxInstance
            {
                TaxName = x.Name,
                TaxAmount = x.CalculateTax(startingAmount - taxFreeCharitySpendings)
            }).ToDictionary(x => x.TaxName, x => x);

            return (taxes, taxFreeCharitySpendings);
        }

        private void AddTax<T>() where T : ITax, new()
        {
            var taxName = typeof(T).Name;
            _taxes.Add(
                taxName,
                new T()
                {
                    Name = taxName,
                    Settings = _taxSettingsService.GetTaxSettings(taxName)
                });
        }
    }
}
