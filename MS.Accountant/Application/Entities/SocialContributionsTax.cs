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
            return Math.Min(amount > 1000.0m ? amount - 1000m : 0m, 2000m) * 0.15m;
        }
    }
}
