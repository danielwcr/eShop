namespace eShop.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public int? BuyerId { get; }

    public OrderStatusChangedToCancelledIntegrationEvent(int orderId, OrderStatus orderStatus, int? buyerId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerId = buyerId;
    }
}
