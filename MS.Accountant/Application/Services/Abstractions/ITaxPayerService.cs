using System;
using System.Threading.Tasks;

using MS.Accountant.Application.Entities;

namespace MS.Accountant.Application.Services.Abstractions
{
    public interface ITaxPayerService
    {
        Task<TaxPayer> CreateTaxPayerAsync(string fullName, long ssn, DateTime? dateOfBirth, decimal grossIncome, decimal charitySpent);
    }
}