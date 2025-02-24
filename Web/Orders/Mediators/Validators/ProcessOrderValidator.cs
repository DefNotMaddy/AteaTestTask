using Core.Dtos.Orders;
using FluentValidation;

namespace Web.Orders.Mediators.Validators
{
    public class ProcessOrderValidator : AbstractValidator<OrderRequest>
    {
        public ProcessOrderValidator()
        {
            RuleFor(request => request.OrderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("OrderId cannot be empty or null.");

            RuleFor(request => request.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserId cannot be empty or null.");

            RuleFor(request => request.PayableAmount)
                .NotNull()
                .WithMessage("PayableAmount must be present.");

            RuleFor(request => request.PayableGateway)
                .NotNull()
                .NotEmpty()
                .WithMessage("PayableGateway cannot be empty or null.");

            RuleFor(request => request.OptionalDescription)
                .NotNull()
                .MaximumLength(500)
                .WithMessage("OptionalDescription cannot exceed 500 characters or be null.");
        }
    }
}
