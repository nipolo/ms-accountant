using System;

using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Entities
{
    public class IncomeTax : ITax
    {
        public IncomeTax(TaxSettings settings)
        {
            Settings = settings;
        }

        public int Id => 1;

        public string Name => nameof(IncomeTax);

        public TaxSettings Settings { get; }

        public decimal CalculateTax(decimal amount)
        {
            return Math.Max(amount - 1000m, 0m) * 0.1m;
        }
    }
}
