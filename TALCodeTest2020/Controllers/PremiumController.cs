using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TALCodeTest2020.Models;
using TALCodeTest2020.Services.Interfaces;

namespace TALCodeTest2020.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumController : ControllerBase
    {
        private readonly IPremiumCalculationService _premiumCalculationService;

        private readonly ILogger<PremiumController> _logger;

        public PremiumController(ILogger<PremiumController> logger,
            IPremiumCalculationService premiumCalculationService)
        {
            _logger = logger;
            _premiumCalculationService = premiumCalculationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _premiumCalculationService.GetOccupationRatingsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet]
        [Route("quote")]
        public async Task<IActionResult> Get([FromQuery] PremiumQuoteModel premiumQuote)
        {
            var premium = await _premiumCalculationService.CalculatePremiumAsync(premiumQuote);
            return Ok(premium);
        }
    }
}
