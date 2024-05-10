namespace EnShop.Ordering.Domain.Events;

/// <summary>
/// Event used when the order stock items are confirmed
/// </summary>
public record class OrderStatusChangedToStockConfirmedDomainEvent(Order Order) : INotification;
