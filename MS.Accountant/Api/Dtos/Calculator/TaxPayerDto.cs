using System;

namespace MS.Accountant.Api.Dtos.Calculator
{
    // NOTE: I'd suggest to be named CreateTaxPayerRequestDto
    public class TaxPayerDto
    {
        public string FullName { get; set; }

        public long SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public decimal GrossIncome { get; set; }

        public decimal? CharitySpent { get; set; }
    }
}
