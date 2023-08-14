using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Core.Services;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Interfaces.Lookups;
using Yoma.Core.Domain.Entity.Services;
using Yoma.Core.Domain.Entity.Services.Lookups;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Services;
using Yoma.Core.Domain.Opportunity.Interfaces;
using Yoma.Core.Domain.Opportunity.Interfaces.Lookups;
using Yoma.Core.Domain.Opportunity.Services;
using Yoma.Core.Domain.Opportunity.Services.Lookups;

namespace Yoma.Core.Domain
{
    public static class Startup
    {
        #region Public Members
        public static void ConfigureServices_DomainServices(this IServiceCollection services)
        {
            //register all validators in Yoma.Core.Domain assembly
            services.AddValidatorsFromAssemblyContaining<UserService>();

            #region Core
            services.AddScoped<IS3ObjectService, S3ObjectService>();
            #endregion Core

            #region Entity
            #region Lookups
            services.AddScoped<IOrganizationProviderTypeService, OrganizationProviderTypeService>();
            #endregion Lookups
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IUserService, UserService>();
            #endregion Entity

            #region Lookups
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ITimeIntervalService, TimeIntervalService>();
            #endregion Lookups

            #region Opportunity
            #region Lookups
            services.AddScoped<IOpportunityCategoryService, OpportunityCategoryService>();
            services.AddScoped<IOpportunityDifficultyService, OpportunityDifficultyService>();
            services.AddScoped<IOpportunityStatusService, OpportunityStatusService>();
            services.AddScoped<IOpportunityTypeService, OpportunityTypeService>();
            #endregion Lookups
            services.AddScoped<IOpportunityService, OpportunityService>();
            #endregion Opportunity
        }

        public static void ConfigureServices_RecurringJobs(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var options = configuration.GetSection(ScheduleJobOptions.Section).Get<ScheduleJobOptions>() ?? throw new InvalidOperationException($"Failed to retrieve configuration section '{ScheduleJobOptions.Section}'");
            using var scope = serviceProvider.CreateScope();
            var skillService = scope.ServiceProvider.GetRequiredService<ISkillService>();
            RecurringJob.AddOrUpdate("Skill Reference Seeding", () => skillService.SeedSkills(), options.ScheduleSeedSkills, new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc });
        }

        public static void ConfigureServices_AWSClients(this IServiceCollection services, IConfiguration configuration)
        {
            var aWSSettings = configuration.GetSection(AWSOptions.Section).Get<AWSOptions>() ?? throw new InvalidOperationException($"Failed to retrieve configuration section '{nameof(AWSOptions)}'");
            services.Configure<AWSOptions>(options => configuration.GetSection(nameof(AWSOptions)).Bind(options));

            var aWSS3Options = new Amazon.Extensions.NETCore.Setup.AWSOptions
            {
                Region = RegionEndpoint.GetBySystemName(aWSSettings.S3Region),
                Credentials = new BasicAWSCredentials(aWSSettings.S3AccessKey, aWSSettings.S3SecretKey)
            };

            services.AddAWSService<IAmazonS3>(aWSS3Options);
        }
        #endregion
    }
}
