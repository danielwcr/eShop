namespace EnShop.Ordering.Domain.Events;

public record class AggregateCreatedDomainEvent(Order Order) : INotification;
