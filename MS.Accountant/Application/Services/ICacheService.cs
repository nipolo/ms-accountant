using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services
{
    public interface ICacheService
    {
        TaxPayer GetTaxPayer(long ssn);
        bool SaveTaxPayer(TaxPayer taxPayer);
    }
}