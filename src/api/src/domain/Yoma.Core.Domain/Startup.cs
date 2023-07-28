using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Services;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Services;

namespace Yoma.Core.Domain
{
    public static class Startup
    {
        #region Public Members
        public static void ConfigureServices_DomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            //register all validators in Yoma.Core.Domain assembly
            services.AddValidatorsFromAssemblyContaining<UserService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ICountryService, CountryService>();
        }

        public static void ConfigureServices_RecurringJobs(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
        }
        #endregion
    }
}
