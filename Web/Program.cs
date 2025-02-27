using Core.Extensions;
using Core.Types.ConfigurationTypes;
using System.Reflection;
using Web.Infrastructure.Extensions;


namespace Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true);
            builder.Services.AddJwtAuth(builder.Configuration.GetSection<JwtOptions>(JwtOptions.Section));

            builder.Services.AddSingleton(builder.Configuration.GetSection<JwtOptions>(JwtOptions.Section));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            builder.Services.AddOrderServices();
            var sectionA = builder.Configuration.GetSection<SectionAOptions>(SectionAOptions.Section);

            var app = builder.Build().InitWebApp();
            app.Run();
        }
        private static WebApplication InitWebApp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerUI();

            app.MapControllers();
            // If there are calls to app.UseRouting() and app.UseEndpoints(...), the call to app.UseAuthorization() must go between them.
            return app;
        }
    }
}
