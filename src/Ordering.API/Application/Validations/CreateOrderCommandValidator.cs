namespace EnShop.Ordering.API.Application.Validations;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator(ILogger<CreateOrderCommandValidator> logger)
    {
        RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19);

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
