using MS.Accountant.Domain.Module;

namespace MS.Accountant.Domain.Services.Abstractions
{
    public interface ITaxSettingsService
    {
        decimal GetMaxCharityFreePercent();
        TaxSettings GetTaxSettings(string taxName);
    }
}