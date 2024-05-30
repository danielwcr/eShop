namespace EnShop.Ordering.API.Application.Validations;

public class UpdateCommandCommandValidator : AbstractValidator<UpdateAggregateCommand>
{
    public UpdateCommandCommandValidator(ILogger<UpdateCommandCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
