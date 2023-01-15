using Microsoft.AspNetCore.Mvc;

using MS.Accountant.Api.Dtos.Calculator;
using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Services.Abstractions;

namespace MS.Accountant.Api.Controllers
{
    [Route("api/calculator")]
    [ApiController]
    // NOTE: Consider to be named TaxPayerController
    public class CalculatorController : ControllerBase
    {
        private readonly ITaxPayerService _taxPayerService;
        private readonly ITaxService _taxService;

        public CalculatorController(
            ITaxPayerService taxPayerService,
            ITaxService taxService)
        {
            _taxPayerService = taxPayerService;
            _taxService = taxService;
        }

        // NOTE: Consider to be named tax-payer
        [HttpPost("calculate")]
        // NOTE: Consider to be named CreateTaxPayer
        public ActionResult<TaxesDto> Calculate([FromBody] TaxPayerDto request)
        {
            var taxPayer = _taxPayerService.CreateTaxPayer(
                request.FullName,
                request.SSN,
                request.DateOfBirth,
                request.GrossIncome,
                request.CharitySpent);

            var response = new TaxesDto
            {
                CharitySpent = taxPayer.CharitySpent,
                GrossIncome = taxPayer.GrossIncome,
                NetIncome = taxPayer.NetIncome,
                TotalTax = taxPayer.TotalTaxes,
                IncomeTax = taxPayer.Taxes[nameof(IncomeTax)].TaxAmount,
                SocialTax = taxPayer.Taxes[nameof(SocialContributionsTax)].TaxAmount
            };

            return Ok(response);
        }
    }
}
