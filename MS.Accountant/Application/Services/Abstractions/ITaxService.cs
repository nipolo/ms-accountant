using System.Collections.Generic;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxService
    {
        (List<TaxInstance> Taxes, decimal TaxFreeCharitySpendings) CalculateTaxes(decimal startingAmount, decimal charitySpent);

        ITax FindTaxByName(string taxName);
    }
}