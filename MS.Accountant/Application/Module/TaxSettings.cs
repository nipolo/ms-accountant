namespace MS.Accountant.Application.Module
{
    public class TaxSettings
    {
        public decimal? TaxFreeMaxAmount { get; init; }

        public decimal? MaxTaxableAmount { get; init; }

        public decimal Percent { get; init; }
    }
}
