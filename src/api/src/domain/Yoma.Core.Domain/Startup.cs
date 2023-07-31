using Amazon.S3;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Core.Services;
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
            services.AddScoped<IS3ObjectService, S3ObjectService>();
        }

        public static void ConfigureServices_RecurringJobs(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
        }

        public static void ConfigureServices_AWSClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AWSOptionsS3>(options => configuration.GetSection(nameof(AWSOptionsS3)).Bind(options));
            services.AddAWSService<IAmazonS3>(configuration.GetAWSOptions(nameof(AWSOptionsS3)));
        }
        #endregion
    }
}
