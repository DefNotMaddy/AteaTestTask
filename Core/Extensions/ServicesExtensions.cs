using Core.Types.ConfigurationTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core.Dtos.Responses;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => configuration.GetSection<JwtOptions>(JwtOptions.Section));
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = services.BuildServiceProvider().GetRequiredService<JwtOptions>();
                    var logger = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async context =>
                        {
                            logger.LogInformation("Authentication failed: {0}", context.Exception.Message);

                            if (!context.Response.HasStarted)
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync($"{new NotAuthorized() { ResponseMessage = "Error in the authorization token" }}");
                            }
                        },
                        OnChallenge = async context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                logger.LogInformation("Unauthorized request: Missing or invalid token.");
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync($"{new NotAuthorized() { ResponseMessage = "Invalid or incorrect token" }}");
                            }
                            context.HandleResponse();
                        }
                    };
                });
            return services;
        }
    }
}
