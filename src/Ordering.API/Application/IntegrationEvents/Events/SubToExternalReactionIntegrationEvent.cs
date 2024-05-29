namespace EnShop.Ordering.API.Application.IntegrationEvents.Events;

public record SubToExternalReactionIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public SubToExternalReactionIntegrationEvent(int orderId) => OrderId = orderId;
}
