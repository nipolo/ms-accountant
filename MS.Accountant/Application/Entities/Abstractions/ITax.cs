using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Entities.Abstractions
{
    public interface ITax
    {
        public string Name { get; init; }

        public TaxSettings Settings { get; init; }

        public decimal CalculateTax(decimal amount);
    }
}
