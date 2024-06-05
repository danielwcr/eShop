namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record SubToExternalReactionIntegrationEvent(int OrderId, string UserId) : IntegrationEvent;
