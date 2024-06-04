namespace EnShop.Ordering.API.Application.Validations;

public class UpdateAggregateCommandValidator : AbstractValidator<UpdateAggregateCommand>
{
    public UpdateAggregateCommandValidator(ILogger<UpdateAggregateCommandValidator> logger)
    {
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
