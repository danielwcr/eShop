namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateUpdatedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string UserId { get; }

    public AggregateUpdatedIntegrationEvent(int orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}
