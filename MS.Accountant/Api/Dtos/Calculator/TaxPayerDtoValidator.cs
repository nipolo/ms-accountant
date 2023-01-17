using FluentValidation;

using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace MS.Accountant.Api.Dtos.Calculator
{
    public class TaxPayerDtoValidator : AbstractValidator<TaxPayerDto>
    {
        private const int _minNumberOfSSNDigits = 5;
        private const int _maxNumberOfSSNDigits = 10;
        private static readonly Regex _fullNameRegEx = new(@"^[a-zA-Z]+\s[a-zA-Z]+$");

        public TaxPayerDtoValidator()
        {
            RuleFor(x => x.FullName).Matches(_fullNameRegEx);
            RuleFor(x => x.SSN).ExclusiveBetween(
                (long)BigInteger.Pow(10, _minNumberOfSSNDigits - 1) - 1,
                (long)BigInteger.Pow(10, _maxNumberOfSSNDigits + 1));
            RuleFor(x => x.GrossIncome).GreaterThan(0);
            RuleFor(x => x.CharitySpent).Must(x => !x.HasValue || x.Value >= 0);
            RuleFor(x => x.DateOfBirth).Must(x => !x.HasValue || x.Value < DateTime.Now);
        }
    }
}
