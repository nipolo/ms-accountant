using System;

using MS.Accountant.Domain.Entities.Abstractions;
using MS.Accountant.Domain.Module;

namespace MS.Accountant.Domain.Entities
{
    public class SocialContributionsTax : ITax
    {
        public string Name { get; init; }

        public TaxSettings Settings { get; init; }

        public decimal CalculateTax(decimal amount)
        {
            return Math.Min(
                    Math.Max(amount - Settings.TaxFreeMaxAmount.Value, 0m),
                    Settings.MaxTaxableAmount.Value - Settings.TaxFreeMaxAmount.Value)
                * (Settings.Percent / 100.0m);
        }
    }
}
