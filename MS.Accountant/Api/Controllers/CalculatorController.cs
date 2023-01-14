using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MS.Accountant.Api.Dtos.Calculator;
using MS.Accountant.Application.Services.Abstractions;
using System.Linq;
using MS.Accountant.Application.Entities;

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
                IncomeTax = taxPayer.Taxes.Single(x => x.TaxId == _taxService.FindTaxByName(nameof(IncomeTax)).Id).TaxAmount,
                SocialTax = taxPayer.Taxes.Single(x => x.TaxId == _taxService.FindTaxByName(nameof(SocialContributionsTax)).Id).TaxAmount
            };

            return Ok(response);
        }
    }
}
