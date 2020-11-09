using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TALCodeTest2020.Models;
using TALCodeTest2020.Services.Interfaces;

namespace TALCodeTest2020.Services
{
    public class PremiumCalculationService : IPremiumCalculationService
    {
        private readonly IOccupationRatingService _occupationRatingService;

        public PremiumCalculationService(IOccupationRatingService occupationRatingService)
        {
            _occupationRatingService = occupationRatingService;
        }

        public async Task<PremiumQuoteModel> CalculatePremiumAsync(PremiumQuoteModel premiumQuote)
        {
            if (premiumQuote is null)
                return await Task.FromResult(premiumQuote);

            var age = 0;
            if (DateTime.TryParseExact(premiumQuote.DOB, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                out var value))
            {
                age = (DateTime.Today - value).Days / 365;
            }
            else
                age = premiumQuote.Age;

            premiumQuote.Age = age;
            if (age < 18 || age > 100)
            {
                premiumQuote.Msg = "Invalid age";
                return await Task.FromResult(premiumQuote);
            }

            var rating = await _occupationRatingService.OccupationRatingFactorAsync(premiumQuote.OccupationRating);
            var premium = (premiumQuote.Amount * rating * age) / 1000 * 12;

            premiumQuote.Premium = premium;

            return await Task.FromResult(premiumQuote);
        }
    }
}
