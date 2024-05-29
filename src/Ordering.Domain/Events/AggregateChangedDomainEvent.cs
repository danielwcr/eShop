namespace EnShop.Ordering.Domain.Events;

public record class AggregateChangedDomainEvent(Order Order) : INotification;
