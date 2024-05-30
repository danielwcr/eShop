namespace EnShop.Ordering.API.Application.Validations;

public class CreateCommandCommandValidator : AbstractValidator<CreateAggregateCommand>
{
    public CreateCommandCommandValidator(ILogger<CreateCommandCommandValidator> logger)
    {
        RuleFor(command => command.UserId).NotNull();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
