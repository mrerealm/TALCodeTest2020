using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TALCodeTest2020.Models;

namespace TALCodeTest2020.Services.Interfaces
{
    public interface IPremiumCalculationService
    {
        public Task<PremiumQuoteModel> CalculatePremiumAsync(PremiumQuoteModel premiumQuote);
        public Task<IEnumerable<OccupationRatingModel>> GetOccupationRatingsAsync();
    }
}
