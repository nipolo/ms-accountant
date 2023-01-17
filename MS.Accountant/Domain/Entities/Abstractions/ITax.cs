using MS.Accountant.Domain.Module;

namespace MS.Accountant.Domain.Entities.Abstractions
{
    public interface ITax
    {
        public string Name { get; init; }

        public TaxSettings Settings { get; init; }

        public decimal CalculateTax(decimal amount);
    }
}
