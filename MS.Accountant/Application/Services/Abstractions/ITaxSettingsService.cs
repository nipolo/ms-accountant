using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxSettingsService
    {
        decimal GetMaxCharityFreePercent();
        TaxSettings GetTaxSettings(string taxName);
    }
}