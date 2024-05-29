namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateChangedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string UserId { get; }

    public AggregateChangedIntegrationEvent(int orderId, OrderStatus orderStatus, string userId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        UserId = userId;
    }
}
