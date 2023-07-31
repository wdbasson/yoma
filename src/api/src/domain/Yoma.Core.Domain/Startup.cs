using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using FS.Keycloak.RestApiClient.Client;
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
            var options = configuration.GetSection(nameof(AWSSettings)).Get<AWSSettings>();

            if (options == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section '{nameof(AWSSettings)}'");

            services.Configure<AWSSettings>(options => configuration.GetSection(nameof(AWSSettings)).Bind(options));

            var aWSOptions = new AWSOptions
            {
                Region = RegionEndpoint.GetBySystemName(options.S3Region),
                Credentials = new BasicAWSCredentials(options.S3AccessKey, options.S3SecretKey)
            };

            services.AddAWSService<IAmazonS3>(aWSOptions);
        }
        #endregion
    }
}
