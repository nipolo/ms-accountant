using System.Collections.Generic;

using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxService
    {
        List<TaxInstance> CalculateTaxes(decimal startingAmount);

        int FindTaxIdByName(string taxName);
    }
}