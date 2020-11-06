using System;
using TALCodeTest2020.Services.Interfaces;
using TALCodeTest2020.Models;
using FluentAssertions;

using Xunit;

namespace TALCodeTest2020.Tests
{
    public class PremiumCalculationServiceTests
    {
        private readonly IPremiumCalculationService _premiumCalculationService;

        public PremiumCalculationServiceTests(IPremiumCalculationService premiumCalculationService)
        {
            _premiumCalculationService = premiumCalculationService;
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteIsNull_ThenResultShouldBeNull()
        {
            PremiumQuoteModel premiumQuote = null;
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().BeNull();
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteValuesAreZero_ThenPremiumShouldBeZero()
        {
            var premiumQuote = new PremiumQuoteModel();
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAmountIsZero_ThenPremiumShouldBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 0, Age = 30, DOB = "2000-01-01", OccupationRating = 1 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeAndDOBNotProvided_ThenPremiumShouldBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, OccupationRating = 1 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteOccupationRatingNotProvided_ThenPremiumShouldBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, Age = 30 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteValuesAreProvided_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, Age = 30, DOB = "01/01/2000", OccupationRating = 1 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeIsProvidedButNotDOB_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, Age = 30, OccupationRating = 1 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteDOBIsProvidedButNotAge_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, DOB = "01/01/2000", OccupationRating = 1 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }
    }
}
