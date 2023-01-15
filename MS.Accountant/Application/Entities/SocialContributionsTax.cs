using System;

using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Entities
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
