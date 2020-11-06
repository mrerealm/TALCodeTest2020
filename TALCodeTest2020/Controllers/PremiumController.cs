using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TALCodeTest2020.Models;

namespace TALCodeTest2020.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumController : ControllerBase
    {
        private static readonly decimal[] OccupationRatingFactor = new decimal[]
        {
            1.0M, 1.25M, 1.50M, 1.75M
        };

        private readonly ILogger<PremiumController> _logger;

        public PremiumController(ILogger<PremiumController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PremiumQuoteModel premiumQuote)
        {
            var rating = premiumQuote.OccupationRating >= 1 && premiumQuote.OccupationRating <= 4 ?
                OccupationRatingFactor[premiumQuote.OccupationRating - 1] : 0;
            var premium = (premiumQuote.Amount * rating * premiumQuote.Age) / 1000 * 12;
            return Ok(premium);
        }
    }
}
