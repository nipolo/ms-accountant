using MS.Accountant.Application.Module;

namespace MS.Accountant.Application.Entities.Abstractions
{
    public interface ITax
    {
        public int Id { get; }

        public string Name { get; }

        public TaxSettings Settings { get; }

        public decimal CalculateTax(decimal amount);
    }
}
