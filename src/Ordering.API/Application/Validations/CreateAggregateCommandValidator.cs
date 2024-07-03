namespace EnShop.Ordering.API.Application.Validations;

public class CreateAggregateCommandValidator : AbstractValidator<CreateAggregateCommand>
{
    public CreateAggregateCommandValidator(ILogger<CreateAggregateCommandValidator> logger)
    {
        RuleFor(command => command).NotNull();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

public class CreateAggregateIdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateAggregateCommand, bool>>
{
    public CreateAggregateIdentifiedCommandValidator(ILogger<CreateAggregateIdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
