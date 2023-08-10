using Yoma.Core.Domain.Core.Helpers;
using Yoma.Core.Infrastructure.Database;
using Yoma.Core.Domain;
using Yoma.Core.Domain.Core.Models;
using Microsoft.OpenApi.Models;
using Yoma.Core.Api.Middleware;
using Yoma.Core.Domain.Core.Extensions;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yoma.Core.Domain.Core.Converters;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Yoma.Core.Infrastructure.Keycloak;
using Yoma.Core.Infrastructure.Keycloak.Models;

namespace Yoma.Core.Api
{
    public class Startup
    {
        #region Class Variables
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly Domain.Core.Environment _environment;
        private readonly KeycloakAuthOptions _keycloakAuthOptions;
        private const string _oAuth_Scope_Separator = " ";
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _webHostEnvironment = env;
            _environment = EnvironmentHelper.FromString(_webHostEnvironment.EnvironmentName);
            _keycloakAuthOptions = configuration.Configuration_AuthenticationOptions();
        }
        #endregion

        #region Public Members
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.Configure<AppSettings>(options => 
                _configuration.GetSection(nameof(AppSettings)).Bind(options));
            services.ConfigureServices_Keycloak(_configuration);
            services.AddSingleton<IEnvironmentProvider>(p => ActivatorUtilities.CreateInstance<EnvironmentProvider>(p, _webHostEnvironment.EnvironmentName));
            #endregion Configuration

            #region System
            services.AddControllers(options => options.InputFormatters.Add(new ByteArrayInputFormatter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringTrimmingConverter()));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ReformatValidationProblemAttribute));
            });

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            #endregion

            #region 3rd Party
            ConfigureCORS(services);
            services.ConfigureServices_AuthenticationKeycloak(_configuration);
            ConfigureAuthorization(services, _configuration);
            ConfigureSwagger(services);
            #endregion 3rd Party

            #region Services & Infrastructure
            services.ConfigureServices_DomainServices(_configuration);
            services.ConfigureServices_AWSClients(_configuration);
            services.ConfigureService_InfrastructuresKeycloak();
            services.ConfigureServices_InfrastructureDatabase(_configuration);
            #endregion Services & Infrastructure
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            #region 3rd Party
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v3/swagger.json", $"Yoma Core Api ({_environment.ToDescription()} v3)");
                s.RoutePrefix = string.Empty;
                s.OAuthClientId(_keycloakAuthOptions.ClientId);
                s.OAuthClientSecret(_keycloakAuthOptions.ClientSecret);
                s.OAuthScopeSeparator(_oAuth_Scope_Separator);
            });
            #endregion 3rd Party

            #region System
            app.UseMiddleware<ExceptionResponseMiddleware>();
            app.UseMiddleware<ExceptionLogMiddleware>();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            if(_environment != Domain.Core.Environment.Local) app.UseSentryTracing();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion System

            #region Services & Infrastructure
            // ef migrations
            serviceProvider.Configure_InfrastructureDatabase();
            #endregion Services & Infrastructure
        }
        #endregion

        #region Private Members
        private void ConfigureCORS(IServiceCollection services)
        {
            const string _config_Section = "AllowedOrigins";

            var origins = _configuration.GetSection(_config_Section).Get<string>();
            if (origins == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section 'Config_Section'");
            var values = origins.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
            if (!values.Any())
                throw new InvalidOperationException($"Configuration section '{_config_Section}' contains no configured hosts");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                          builder =>
                          {
                              builder
                            .WithOrigins(values)
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                          });
            });
        }

        private void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Common.Constants.Authorization_Policy, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new RequireClaimAuthorizationRequirement());
                });
            });
            services.AddSingleton<IAuthorizationHandler, RequiredClaimAuthorizationHandler>();
            services.ConfigureServices_AuthorizationKeycloak(configuration);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var appSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            if (appSettings == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section '{nameof(AppSettings)}'");

            var scopes = appSettings.SwaggerScopes.Split(_oAuth_Scope_Separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
            if (!scopes.Any())
                throw new InvalidOperationException($"Configuration section '{nameof(AppSettings)}' contains no configured swagger scopes");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo { Title = $"Yoma Core Api ({_environment.ToDescription()})", Version = "v3" });
                c.EnableAnnotations();
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = $"Keycloak JWT Authorization header using the {JwtBearerDefaults.AuthenticationScheme} scheme. Example: Authorization: {JwtBearerDefaults.AuthenticationScheme} {{token}}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            Scopes = scopes.ToDictionary(item => item, item => item),
                            TokenUrl = _keycloakAuthOptions.TokenUrl,
                            AuthorizationUrl = _keycloakAuthOptions.AuthorizationUrl
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
                        },
                        new[] { string.Join(_oAuth_Scope_Separator, scopes) }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = SecuritySchemeType.OAuth2.ToString(),
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
        #endregion
    }
}

