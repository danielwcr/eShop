namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record AggregateChangedIntegrationEvent(int OrderId) : IntegrationEvent;
