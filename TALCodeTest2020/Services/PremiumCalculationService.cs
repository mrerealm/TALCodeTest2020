using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TALCodeTest2020.Models;
using TALCodeTest2020.Services.Interfaces;

namespace TALCodeTest2020.Services
{
    public class PremiumCalculationService : IPremiumCalculationService
    {
        private static readonly decimal[] OccupationRatingFactor = new decimal[]
        {
            1.0M, 1.25M, 1.50M, 1.75M
        };

        public Task<PremiumQuoteModel> CalculatePremiumAsync(PremiumQuoteModel premiumQuote)
        {
            if (premiumQuote is null)
                return Task.FromResult<PremiumQuoteModel>(premiumQuote);

            var age = 0;
            if (DateTime.TryParseExact(premiumQuote.DOB, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                out var value))
            {
                age = (DateTime.Today - value).Days / 365;
            }
            else
                age = premiumQuote.Age;

            var rating = premiumQuote.OccupationRating >= 1 && premiumQuote.OccupationRating <= 4 ?
                OccupationRatingFactor[premiumQuote.OccupationRating - 1] : 0;
            var premium = (premiumQuote.Amount * rating * age) / 1000 * 12;

            premiumQuote.Age = age;
            premiumQuote.Premium = premium;

            return Task.FromResult<PremiumQuoteModel>(premiumQuote);
        }

        public Task<IEnumerable<OccupationRatingModel>> GetOccupationRatingsAsync()
        {
            var result = new List<OccupationRatingModel>()
            {
                new OccupationRatingModel{ Occupation = "Cleaner", Rating = 3},
                new OccupationRatingModel{ Occupation = "Doctor", Rating = 1},
                new OccupationRatingModel{ Occupation = "Author", Rating = 2},
                new OccupationRatingModel{ Occupation = "Farmer", Rating = 4},
                new OccupationRatingModel{ Occupation = "Mechanic", Rating = 4},
                new OccupationRatingModel{ Occupation = "Florist", Rating = 3},
            };

            return Task.FromResult<IEnumerable<OccupationRatingModel>>(result);
        }
    }
}
