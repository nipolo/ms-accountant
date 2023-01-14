using System;

using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Entities
{
    public class SocialContributionsTax : ITax
    {
        public SocialContributionsTax(TaxSettings settings)
        {
            Settings = settings;
        }

        public int Id => 2;

        public string Name => nameof(SocialContributionsTax);

        public TaxSettings Settings { get; }

        public decimal CalculateTax(decimal amount)
        {
            return Math.Min(
                    Math.Max(amount - Settings.TaxFreeMaxAmount.Value, 0m),
                    Settings.MaxTaxableAmount.Value - Settings.TaxFreeMaxAmount.Value)
                * (Settings.Percent / 100.0m);
        }
    }
}
