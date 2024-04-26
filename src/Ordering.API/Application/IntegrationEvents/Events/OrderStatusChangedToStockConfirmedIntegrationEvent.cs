namespace eShop.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public int? BuyerId { get; }

    public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId, OrderStatus orderStatus, int? buyerId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerId = buyerId;
    }
}
