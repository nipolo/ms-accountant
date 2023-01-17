using System.Collections.Generic;

using MS.Accountant.Domain.Entities;

namespace MS.Accountant.Domain.Services.Abstractions
{
    public interface ITaxService
    {
        (Dictionary<string, TaxInstance> Taxes, decimal TaxFreeCharitySpendings) CalculateTaxes(decimal startingAmount, decimal charitySpent);
    }
}