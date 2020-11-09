using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TALCodeTest2020.Models;

namespace TALCodeTest2020.Services.Interfaces
{
    public interface IOccupationRatingService
    {
        public Task<IEnumerable<OccupationRatingModel>> GetOccupationRatingsAsync();
        public Task<decimal> OccupationRatingFactorAsync(int occupationRating);

    }
}
