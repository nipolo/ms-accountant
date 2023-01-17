using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using MS.Accountant.Application.Entities.Abstractions;
using MS.Accountant.Application.Exceptions;

namespace MS.Accountant.Application.Entities
{
    public class TaxPayer : IEntity
    {
        private const int MinNumberOfSSNDigits = 5;
        private const int MaxNumberOfSSNDigits = 10;
        private readonly Regex _fullNameRegEx = new(@"^[a-zA-Z]+\s[a-zA-Z]+$");

        public TaxPayer(
            string fullName,
            long ssn,
            DateTime? dateOfBirth,
            decimal grossIncome,
            decimal charitySpent,
            Dictionary<string, TaxInstance> taxes,
            decimal taxFreeCharitySpendings)
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
