using Yoma.Core.Domain.Core.Helpers;
using Yoma.Core.Infrastructure.Database;
using Yoma.Core.Domain;
using Yoma.Core.Domain.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Yoma.Core.Api.Middleware;
using Yoma.Core.Domain.Core.Extensions;
using Flurl;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Yoma.Core.Domain.Core.Converters;

namespace Yoma.Core.Api
{
    public class Startup
    {
        #region Class Variables
        private IWebHostEnvironment _webHostEnvironment { get; }
        private IConfiguration _configuration { get; }
        private Domain.Core.Environment _environment { get; }
        private KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
        private const string _oAuth_Scope_Separator = " ";
        #endregion

        #region Constructors
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _webHostEnvironment = env;
            _environment = EnvironmentHelper.FromString(_webHostEnvironment.EnvironmentName);

            var keycloakAuthenticationOptions = _configuration
               .GetSection(KeycloakAuthenticationOptions.Section)
               .Get<KeycloakAuthenticationOptions>();

            if (keycloakAuthenticationOptions == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section '{KeycloakAuthenticationOptions.Section}.{nameof(KeycloakAuthenticationOptions)}'");

            _keycloakAuthenticationOptions = keycloakAuthenticationOptions;
        }
        #endregion

        #region Public Members
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration
            services.Configure<AppSettings>(options => 
                _configuration.GetSection(nameof(AppSettings)).Bind(options));
            services.Configure<KeycloakAuthenticationOptions>(options => 
                _configuration.GetSection(KeycloakAuthenticationOptions.Section).Bind(options));
            #endregion Configuration

            #region System
            services.AddControllers(options => options.InputFormatters.Add(new ByteArrayInputFormatter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringTrimmingConverter()));

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            #endregion

            #region 3rd Party
            var keycloakProtectionClientOptions = _configuration
                .GetSection(KeycloakProtectionClientOptions.Section).Get<KeycloakProtectionClientOptions>();

            if (keycloakProtectionClientOptions == null)
                throw new InvalidOperationException($"Failed to retrieve config section '{KeycloakProtectionClientOptions.Section}.{nameof(KeycloakProtectionClientOptions)}'");

            ConfigureCORS(services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(services, keycloakProtectionClientOptions);
            ConfigureSwagger(services);
            #endregion 3rd Party

            #region Services & Infrastructure
            services.ConfigureServices_DomainServices(_configuration);
            services.ConfigureServices_InfrastructureDatabase(_configuration);
            #endregion Services & Infrastructure
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var keycloakAuthenticationOptions = _configuration
                .GetSection(KeycloakAuthenticationOptions.Section).Get<KeycloakAuthenticationOptions>();

            if (keycloakAuthenticationOptions == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section '{KeycloakAuthenticationOptions.Section}'");

            #region 3rd Party
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v3/swagger.json", $"Yoma Core Api ({_environment.ToDescription()} v3)");
                s.RoutePrefix = string.Empty;
                s.OAuthClientId(keycloakAuthenticationOptions.Resource);
                s.OAuthClientSecret(keycloakAuthenticationOptions.Credentials.Secret);
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

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddKeycloakAuthentication(_keycloakAuthenticationOptions);
        }

        private void ConfigureAuthorization(IServiceCollection services, KeycloakProtectionClientOptions options)
        {
            services.AddAuthorization().AddKeycloakAuthorization(options);
            services.AddTransient<IClaimsTransformation, KeyCloakClaimsTransformer>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var appSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            if (appSettings == null)
                throw new InvalidOperationException($"Failed to retrieve configuration section '{nameof(AppSettings)}'");

            var scopes = appSettings.SwaggerScopes.Split(_oAuth_Scope_Separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
            if (!scopes.Any())
                throw new InvalidOperationException($"Configuration section '{nameof(AppSettings)}' contains no configured swagger scopes");

            var tokenUri = _keycloakAuthenticationOptions.AuthServerUrl
                .AppendPathSegment("realms")
                .AppendPathSegment(_keycloakAuthenticationOptions.Realm)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token").ToUri();

            var authUri = _keycloakAuthenticationOptions.AuthServerUrl
                .AppendPathSegment("realms")
                .AppendPathSegment(_keycloakAuthenticationOptions.Realm)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("auth").ToUri();

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
                            TokenUrl = tokenUri,
                            AuthorizationUrl = authUri
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

