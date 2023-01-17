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
        public const int MinNumberOfSSNDigits = 5;
        public const int MaxNumberOfSSNDigits = 10;
        public static readonly Regex FullNameRegEx = new(@"^[a-zA-Z]+\s[a-zA-Z]+$");

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
