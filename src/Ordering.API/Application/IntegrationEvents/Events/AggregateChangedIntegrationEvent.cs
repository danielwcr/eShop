namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateChangedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string UserId { get; }

    public AggregateChangedIntegrationEvent(int orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}
