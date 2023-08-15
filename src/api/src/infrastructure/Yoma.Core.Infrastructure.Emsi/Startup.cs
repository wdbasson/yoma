using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yoma.Core.Domain.Emsi.Interfaces;
using Yoma.Core.Infrastructure.Emsi.Client;
using Yoma.Core.Infrastructure.Emsi.Models;

namespace Yoma.Core.Infrastructure.Emsi
{
    public static class Startup
    {
        public static void ConfigureServices_Emsi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmsiOptions>(options => configuration.GetSection(EmsiOptions.Section).Bind(options));
        }

        public static void ConfigureService_InfrastructuresEmsi(this IServiceCollection services)
        {
            services.AddScoped<IEmsiClientFactory, EmsiClientFactory>();
        }
    }
}
