using System;

using MS.Accountant.Domain.Entities.Abstractions;
using MS.Accountant.Domain.Module;

namespace MS.Accountant.Domain.Entities
{
    public class IncomeTax : ITax
    {
        public string Name { get; init; }

        public TaxSettings Settings { get; init; }

        public decimal CalculateTax(decimal amount)
        {
            return Math.Max(amount - Settings.TaxFreeMaxAmount.Value, 0m)
                * (Settings.Percent / 100.0m);
        }
    }
}
