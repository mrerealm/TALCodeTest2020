using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TALCodeTest2020.Models;
using TALCodeTest2020.Services.Interfaces;

namespace TALCodeTest2020.Services
{
    public class OccupationRatingService: IOccupationRatingService
    {
        private static readonly decimal[] OccupationRatingFactor = new decimal[]
        {
            1.0M, 1.25M, 1.50M, 1.75M
        };

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

        public Task<decimal> OccupationRatingFactorAsync(int occupationRating)
        {
            return Task.FromResult(occupationRating >= 1 && occupationRating <= 4 ?
                OccupationRatingFactor[occupationRating - 1] : 0);
        }
    }
}
