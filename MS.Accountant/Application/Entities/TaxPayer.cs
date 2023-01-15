using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using MS.Accountant.Application.Exceptions;

namespace MS.Accountant.Application.Entities
{
    public class TaxPayer
    {
        private const int MinNumberOfSSNDigits = 5;
        private const int MaxNumberOfSSNDigits = 10;
        private readonly Regex _fullNameRegEx = new(@"^[a-zA-Z]+\s[a-zA-Z]+$");

        public TaxPayer(
            string fullName,
            long ssn,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent)
        {
            if (!_fullNameRegEx.IsMatch(fullName))
            {
                throw new InvalidArgumentDomainException(nameof(fullName), fullName);
            }

            if (ssn < Math.Pow(10, MinNumberOfSSNDigits - 1)
                || ssn >= Math.Pow(10, MaxNumberOfSSNDigits + 1))
            {
                throw new InvalidArgumentDomainException(nameof(ssn), ssn);
            }

            if (grossIncome < 0)
            {
                throw new InvalidArgumentDomainException(nameof(grossIncome), grossIncome);
            }

            if (charitySpent < 0)
            {
                throw new InvalidArgumentDomainException(nameof(charitySpent), charitySpent);
            }

            if (dateOfBirth.HasValue && dateOfBirth.Value > DateTime.Now)
            {
                throw new InvalidArgumentDomainException(nameof(dateOfBirth), dateOfBirth);
            }

            FullName = fullName;
            SSN = ssn;
            DateOfBirth = dateOfBirth;
            GrossIncome = grossIncome;
            CharitySpent = charitySpent;
            Taxes = new Dictionary<string, TaxInstance>();
        }

        public string FullName { get; }

        public long SSN { get; }

        public DateTime? DateOfBirth { get; }

        public decimal GrossIncome { get; }

        public decimal CharitySpent { get; }

        public decimal TaxFreeCharitySpendings { get; set; }

        public Dictionary<string, TaxInstance> Taxes { get; set; }

        public decimal TotalTaxes => Taxes.Sum(x => x.Value.TaxAmount);

        public decimal NetIncome => GrossIncome - Taxes.Sum(x => x.Value.TaxAmount);
    }
}
