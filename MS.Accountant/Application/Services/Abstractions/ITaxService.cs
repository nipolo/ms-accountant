using System.Collections.Generic;

using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxService
    {
        (Dictionary<string, TaxInstance> Taxes, decimal TaxFreeCharitySpendings) CalculateTaxes(decimal startingAmount, decimal charitySpent);
    }
}