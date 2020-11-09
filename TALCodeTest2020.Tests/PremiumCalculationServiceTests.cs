using TALCodeTest2020.Services.Interfaces;
using TALCodeTest2020.Models;
using FluentAssertions;

using Xunit;
using AutoFixture;

namespace TALCodeTest2020.Tests
{
    public class PremiumCalculationServiceTests
    {
        private static readonly Fixture Fixture = new Fixture();
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
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                    .With(p => p.Amount, 0)
                    .With(p => p.Age, 30)
                    .With(p => p.DOB, "2000-01-01")
                    .With(p => p.OccupationRating, 1)
                    .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeAndDOBNotProvided_ThenPremiumShouldBeZero()
        {
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                     .With(p => p.Amount, 1000)
                     .With(p => p.Age, 0)
                     .With(p => p.DOB, "")
                     .With(p => p.OccupationRating, 1)
                     .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteOccupationRatingNotProvided_ThenPremiumShouldBeZero()
        {
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                      .With(p => p.Amount, 1000)
                      .With(p => p.Age, 30)
                      .With(p => p.DOB, "")
                      .With(p => p.OccupationRating, 0)
                      .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteValuesAreProvided_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                      .With(p => p.Amount, 1000)
                      .With(p => p.Age, 30)
                      .With(p => p.DOB, "")
                      .With(p => p.OccupationRating, 1)
                      .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeIsProvidedButNotDOB_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                    .With(p => p.Amount, 1000)
                    .With(p => p.Age, 30)
                    .With(p => p.DOB, "")
                    .With(p => p.OccupationRating, 1)
                    .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteDOBIsProvidedButNotAge_ThenPremiumShouldNotBeZero()
        {
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                    .With(p => p.Amount, 1000)
                    .With(p => p.Age, 0)
                    .With(p => p.DOB, "01/01/2000")
                    .With(p => p.OccupationRating, 1)
                    .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteRatingIsInvalid_ThenPremiumShouldBeZero()
        {
            var premiumQuote = new PremiumQuoteModel() { Amount = 10, DOB = "01/01/2000", OccupationRating = 0 };
            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);
            premiumQuote.OccupationRating = 10;
            var result2 = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);

            result2.Should().NotBeNull();
            result2.Premium.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeAndDOBAreProvided_ThenDOBIsUsedAndPremiumShouldNotBeZero()
        {
            var invalidAge = 10;

            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                    .With(p => p.Amount, 1000)
                    .With(p => p.Age, invalidAge)
                    .With(p => p.DOB, "01/01/2000")
                    .With(p => p.OccupationRating, 1)
                    .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().NotBe(0);
            result.Age.Should().NotBe(invalidAge);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenPremiumQuoteAgeIsLessThen18_Or_GreaterThen100_ThenPremiumShouldBeZero()
        {
            var invalidAge = 101;
            var premiumQuote = Fixture.Build<PremiumQuoteModel>()
                  .With(p => p.Amount, 1000)
                  .With(p => p.Age, invalidAge)
                  .With(p => p.DOB, "")
                  .With(p => p.OccupationRating, 1)
                  .Create();

            var result = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);
            premiumQuote.Age = 17;
            var result2 = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);

            result.Should().NotBeNull();
            result.Premium.Should().Be(0);
            result.Msg.Should().NotBeNull().Should().NotBe("");

            result2.Should().NotBeNull();
            result2.Premium.Should().Be(0);
            result2.Msg.Should().NotBeNull().Should().NotBe("");
        }

    }
}
