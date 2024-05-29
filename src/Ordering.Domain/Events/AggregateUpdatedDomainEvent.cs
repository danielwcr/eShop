namespace EnShop.Ordering.Domain.Events;

public record class AggregateUpdatedDomainEvent(Order Order) : INotification;
