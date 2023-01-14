using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Module;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Application.Services
{
    public class TaxService : ITaxService
    {
        private readonly List<ITax> _taxes = new();

        public TaxService(IOptions<TaxesSettings> taxesSettings)
        {
            _taxes.Add(new IncomeTax(taxesSettings.Value.Taxes[nameof(IncomeTax)]));
            _taxes.Add(new SocialContributionsTax(taxesSettings.Value.Taxes[nameof(SocialContributionsTax)]));
        }

        public List<TaxInstance> CalculateTaxes(decimal startingAmount)
        {
            return _taxes.Select(x => new TaxInstance
            {
                TaxId = x.Id,
                TaxAmount = x.CalculateTax(startingAmount)
            }).ToList();
        }

        public int FindTaxIdByName(string taxName)
        {
            return _taxes.FirstOrDefault(x => x.Name == taxName).Id;
        }
    }
}
