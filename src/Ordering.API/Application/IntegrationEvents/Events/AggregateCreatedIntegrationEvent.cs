namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateCreatedIntegrationEvent : IntegrationEvent
{
    public string UserId { get; init; }

    public AggregateCreatedIntegrationEvent(string userId)
        => UserId = userId;
}
