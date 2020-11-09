using TALCodeTest2020.Services.Interfaces;
using TALCodeTest2020.Models;
using FluentAssertions;

using Xunit;
using System.Linq;

namespace TALCodeTest2020.Tests
{
    public class OccupationRatingServiceTests
    {
        private readonly IOccupationRatingService _occupationRatingService;

        public OccupationRatingServiceTests(IOccupationRatingService occupationRatingService)
        {
            _occupationRatingService = occupationRatingService;
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenGetOccupationRatingsList_ThenResultShouldNotBeNullOrEmpty()
        {
            var result = await _occupationRatingService.GetOccupationRatingsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task WhenGetOccupationRatingFactorWithinRange_ThenResultShouldNotBeZero()
        {
            var list = await _occupationRatingService.GetOccupationRatingsAsync();
            list.Should().NotBeNull();
            list.Should().HaveCountGreaterOrEqualTo(1);

            var orderedRatings = list.ToList().OrderBy(l => l.Rating);
 
            var result = await _occupationRatingService.OccupationRatingFactorAsync(orderedRatings.First().Rating);
            var result2 = await _occupationRatingService.OccupationRatingFactorAsync(orderedRatings.Last().Rating);

            result.Should().NotBe(0);
            result2.Should().NotBe(0);

        }

        [Fact]
        public async System.Threading.Tasks.Task WhenGetOccupationRatingFactorOutofRange_ThenResultShouldBeZero()
        {
            var list = await _occupationRatingService.GetOccupationRatingsAsync();
            list.Should().NotBeNull();
            list.Should().HaveCountGreaterOrEqualTo(1);

            var orderedRatings = list.ToList().OrderBy(l => l.Rating);

            decimal expectedResult = 0;
            var result = await _occupationRatingService.OccupationRatingFactorAsync(orderedRatings.First().Rating-1);
            var result2 = await _occupationRatingService.OccupationRatingFactorAsync(orderedRatings.Last().Rating+1);

            result.Should().Be(expectedResult);
            result2.Should().Be(expectedResult);

        }
    }
}