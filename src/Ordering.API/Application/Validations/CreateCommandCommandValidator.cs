namespace EnShop.Ordering.API.Application.Validations;

public class CreateCommandCommandValidator : AbstractValidator<CreateAggregateCommand>
{
    public CreateCommandCommandValidator(ILogger<CreateCommandCommandValidator> logger)
    {
        RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19);

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
