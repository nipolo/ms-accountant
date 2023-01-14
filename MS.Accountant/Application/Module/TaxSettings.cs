namespace MS.Accountant.Application.Module
{
    public class TaxSettings
    {
        public decimal? TaxFreeMaxAmount { get; set; }

        public decimal? MaxTaxableAmount { get; set; }

        public decimal Percent { get; set; }
    }
}
