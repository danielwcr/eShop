namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string UserId { get; }

    public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId, OrderStatus orderStatus, string userId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        UserId = userId;
    }
}
