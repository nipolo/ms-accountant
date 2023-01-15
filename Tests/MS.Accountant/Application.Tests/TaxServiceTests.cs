using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using Moq;

using MS.Accountant.Application.Entities;
using MS.Accountant.Application.Module;
using MS.Accountant.Application.Services;
using MS.Accountant.Application.Services.Abstractions;

using NUnit.Framework;

namespace MS.Accountant.Application.Tests
{
    public class Tests
    {
        private ITaxService _taxService;

        [OneTimeSetUp]
        public void Setup()
        {
            var taxesSettingsOptions = Mock.Of<IOptionsMonitor<TaxesSettings>>(x => x.CurrentValue == new TaxesSettings()
            {
                MaxCharityFreePercent = 10m,
                Taxes = new Dictionary<string, TaxSettings>
                {
                    { "IncomeTax",
                        new TaxSettings
                        {
                            MaxTaxableAmount = null,
                            Percent = 10,
                            TaxFreeMaxAmount = 1000
                        }
                    },

                    { "SocialContributionsTax",
                        new TaxSettings
                        {
                            MaxTaxableAmount = 3000,
                            Percent = 15,
                            TaxFreeMaxAmount = 1000
                        }
                    }
                }
            });
            var taxSettingsService = new TaxSettingsService(taxesSettingsOptions);

            _taxService = new TaxService(taxSettingsService);
        }

        [TestCase(980, 0, 0, 0, 0)]
        [TestCase(3400, 0, 0, 240, 300)]
        [TestCase(2500, 150, 150, 135, 202.5)]
        [TestCase(3600, 520, 360, 224, 300)]
        [TestCase(1020, 80, 80, 0, 0)]
        [TestCase(1100, 50, 50, 5, 7.5)]
        public void GrossAmount_Calculate_TaxesCalculated(
            decimal grossAmount, 
            decimal charitySpent,
            decimal taxFreeCharitySpendingsExpected,
            decimal incomeTaxExpectedAmount,
            decimal socialContributionsTaxExpectedAmount)
        {
            // Arrange

            // Act
            var (taxes, taxFreeCharitySpendings) = _taxService.CalculateTaxes(grossAmount, charitySpent);

            // Assert
            Assert.That(taxFreeCharitySpendings, Is.EqualTo(taxFreeCharitySpendingsExpected));

            Assert.That(taxes.Count, Is.EqualTo(2));

            var incomeTaxAmount = taxes[nameof(IncomeTax)].TaxAmount;
            Assert.That(incomeTaxAmount, Is.EqualTo(incomeTaxExpectedAmount));

            var socialContributionsTaxAmount = taxes[nameof(SocialContributionsTax)].TaxAmount;
            Assert.That(socialContributionsTaxAmount, Is.EqualTo(socialContributionsTaxExpectedAmount));
        }
    }
}