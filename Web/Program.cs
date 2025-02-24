using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Web.Infrastructure.Extensions;
using Web.Orders.Mediators.Validators;

namespace Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container (this replaces ConfigureServices in Startup.cs)
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();  // Example of adding Swagger

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            builder.Services.AddOrderServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline (this replaces Configure in Startup.cs)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();


        }
    }
}
