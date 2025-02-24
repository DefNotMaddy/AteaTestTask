using Core.Dtos.Orders;
using Core.Interfaces;
using Core.Services;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Web.Orders.Mediators.Handlers;
using Web.Orders.Mediators.Queries;
using Web.Orders.Mediators.Validators;

namespace Web.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services) => services
            .AddScoped<IOrderProcessor, OrderProcessor>()
            .AddScoped<IRequestHandler<ProcessOrderQuery, OrderResponse>, ProcessOrderHandler>()
            .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(typeof(ProcessOrderValidator).Assembly);
    }
}
