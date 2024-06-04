namespace EnShop.Ordering.API.Application.Validations;

public class UpdateAggregateCommandValidator : AbstractValidator<UpdateAggregateCommand>
{
    public UpdateAggregateCommandValidator(ILogger<UpdateAggregateCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
