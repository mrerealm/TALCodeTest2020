using System;
using Microsoft.Extensions.DependencyInjection;
using TALCodeTest2020.Services;
using TALCodeTest2020.Services.Interfaces;

namespace TALCodeTest2020.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPremiumCalculationService, PremiumCalculationService>();
            services.AddTransient<IOccupationRatingService, OccupationRatingService>();
        }
    }
}
