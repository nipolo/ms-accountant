using Microsoft.Extensions.Options;

using MS.Accountant.Domain.Module;
using MS.Accountant.Domain.Services.Abstractions;

namespace MS.Accountant.Domain.Services
{
    // TODO: Implement tax settings to load from the DB
    public class TaxSettingsService : ITaxSettingsService
    {
        private readonly IOptionsMonitor<TaxesSettings> _taxesSettings;

        public TaxSettingsService(IOptionsMonitor<TaxesSettings> taxesSettings)
        {
            _taxesSettings = taxesSettings;
        }

        public TaxSettings GetTaxSettings(string taxName)
        {
            if (!_taxesSettings.CurrentValue.Taxes.TryGetValue(taxName, out var taxSettings))
            {
                return null;
            };

            return new TaxSettings
            {
                MaxTaxableAmount = taxSettings.MaxTaxableAmount,
                Percent = taxSettings.Percent,
                TaxFreeMaxAmount = taxSettings.TaxFreeMaxAmount
            };
        }

        public decimal GetMaxCharityFreePercent()
        {
            return _taxesSettings.CurrentValue.MaxCharityFreePercent;
        }
    }
}
