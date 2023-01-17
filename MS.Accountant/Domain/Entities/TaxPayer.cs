using System;
using System.Collections.Generic;
using System.Linq;
using MS.Accountant.Domain.Entities.Abstractions;

namespace MS.Accountant.Domain.Entities
{
    public class TaxPayer : IEntity
    {
        public TaxPayer(
            string fullName,
            long ssn,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent,
            Dictionary<string, TaxInstance> taxes,
            decimal taxFreeCharitySpendings)
        {
            FullName = fullName;
            SSN = ssn;
            DateOfBirth = dateOfBirth;
            GrossIncome = grossIncome;
            CharitySpent = charitySpent;
            Taxes = taxes;
            TaxFreeCharitySpendings = taxFreeCharitySpendings;
        }
        public long Id => SSN;

        public string FullName { get; private set; }

        public long SSN { get; }

        public DateTime? DateOfBirth { get; private set; }

        public decimal GrossIncome { get; private set; }

        public decimal CharitySpent { get; private set; }

        public decimal TaxFreeCharitySpendings { get; private set; }

        public Dictionary<string, TaxInstance> Taxes { get; private set; }

        public decimal TotalTaxes => Taxes.Sum(x => x.Value.TaxAmount);

        public decimal NetIncome => GrossIncome - Taxes.Sum(x => x.Value.TaxAmount);

        public bool Equals(
            string fullName,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent)
        {
            return FullName == fullName
                && DateOfBirth == dateOfBirth
                && GrossIncome == grossIncome
                && CharitySpent == charitySpent;
        }
    }
}
