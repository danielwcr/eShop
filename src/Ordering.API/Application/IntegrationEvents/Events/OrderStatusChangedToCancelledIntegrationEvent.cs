namespace eShop.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string UserId { get; }

    public OrderStatusChangedToCancelledIntegrationEvent(int orderId, OrderStatus orderStatus, string userId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        UserId = userId;
    }
}
