using Microsoft.Extensions.Options;

using MS.Accountant.Application.Module;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
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
