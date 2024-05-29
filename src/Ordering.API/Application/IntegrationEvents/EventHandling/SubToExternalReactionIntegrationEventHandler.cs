namespace EnShop.Ordering.API.Application.IntegrationEvents.EventHandling;

public class SubToExternalReactionIntegrationEventHandler(
    IMediator mediator,
    ILogger<SubToExternalReactionIntegrationEventHandler> logger) :
    IIntegrationEventHandler<SubToExternalReactionIntegrationEvent>
{
    public async Task Handle(SubToExternalReactionIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new ChangeAggregateCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}
