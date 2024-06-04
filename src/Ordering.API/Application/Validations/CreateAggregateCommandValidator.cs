namespace EnShop.Ordering.API.Application.Validations;

public class CreateAggregateCommandValidator : AbstractValidator<CreateAggregateCommand>
{
    public CreateAggregateCommandValidator(ILogger<CreateAggregateCommandValidator> logger)
    {
        RuleFor(command => command.UserId).NotNull();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
