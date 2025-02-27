using Core.Types.ConfigurationTypes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
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
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");

                            if (!context.Response.HasStarted)
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync($"{{\"error\": \"Authentication failed: {context.Exception.Message}\"}}");
                            }
                        },
                        OnChallenge = async context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                Console.WriteLine("Unauthorized request: Missing or invalid token.");
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("{\"error\": \"Unauthorized: Missing or invalid token.\"}");
                            }
                            context.HandleResponse(); // ✅ Prevents default challenge response
                        }
                    };
                });

            return services;
        }
    }
}
