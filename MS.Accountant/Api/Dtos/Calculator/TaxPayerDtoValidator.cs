using FluentValidation;

using MS.Accountant.Application.Entities;

using System;
using System.Numerics;

namespace MS.Accountant.Api.Dtos.Calculator
{
    public class TaxPayerDtoValidator : AbstractValidator<TaxPayerDto>
    {
        public TaxPayerDtoValidator()
        {
            RuleFor(x => x.FullName).Matches(TaxPayer.FullNameRegEx);
            RuleFor(x => x.SSN).ExclusiveBetween(
                (long)BigInteger.Pow(10, TaxPayer.MinNumberOfSSNDigits - 1) - 1,
                (long)BigInteger.Pow(10, TaxPayer.MaxNumberOfSSNDigits + 1));
            RuleFor(x => x.GrossIncome).GreaterThan(0);
            RuleFor(x => x.CharitySpent).Must(x => !x.HasValue || x.Value >= 0);
            RuleFor(x => x.DateOfBirth).Must(x => !x.HasValue || x.Value < DateTime.Now);
        }
    }
}
