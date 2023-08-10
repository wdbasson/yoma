using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Lookups.Models;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Infrastructure.Database.Entity.Repositories;
using Yoma.Core.Infrastructure.Database.Lookups.Repositories;

namespace Yoma.Core.Infrastructure.Database
{
    public static class Startup
    {
        #region Public Members
        public static void ConfigureServices_InfrastructureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // infrastructure
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLConnection")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            // repositories
            services.AddScoped<IRepository<S3Object>, S3ObjectRepository>();

            services.AddScoped<IRepository<OrganizationProviderType>, OrganizationProviderTypeRepository>();
            services.AddScoped<IRepository<Organization>, OrganizationRepository>();
            services.AddScoped<IRepository<OrganizationUser>, OrganizationUserRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<UserSkill>, UserSkillRepository>();

            services.AddScoped<IRepository<Country>, CountryRepository>();
            services.AddScoped<IRepository<Gender>, GenderRepository>();
            services.AddScoped<IRepository<ProviderType>, ProviderTypeRepository>();
            services.AddScoped<IRepository<Skill>, SkillRepository>();
        }

        public static void Configure_InfrastructureDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ApplicationDbContext>>();
                if (logger == null)
                    throw new InvalidOperationException($"Failed to get an instance of the service '{nameof(ILogger<ApplicationDbContext>)}'");

                logger.LogDebug("Applying database migrations...");

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if(context == null)
                    throw new InvalidOperationException($"Failed to get an instance of the service '{nameof(ILogger<ApplicationDbContext>)}'");

                // migrate db
                context.Database.Migrate();
            }
        }
        #endregion
    }
}
