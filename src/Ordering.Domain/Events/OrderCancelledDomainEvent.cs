namespace EnShop.Ordering.Domain.Events;

public record class OrderCancelledDomainEvent(Order Order) : INotification;
