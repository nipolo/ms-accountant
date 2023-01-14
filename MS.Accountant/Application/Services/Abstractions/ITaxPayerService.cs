using System;

using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxPayerService
    {
        TaxPayer CreateTaxPayer(string fullName, long ssn, DateTime? dateOfBirth, decimal grossIncome, decimal charitySpent);
    }
}