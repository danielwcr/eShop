namespace EnShop.Ordering.API.Application.DomainEventHandlers;

public class AggregateChangedDomainEventHandler
                : INotificationHandler<AggregateChangedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public AggregateChangedDomainEventHandler(
        IOrderRepository orderRepository,
        ILogger<AggregateChangedDomainEventHandler> logger,
        IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    public async Task Handle(AggregateChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new AggregateChangedIntegrationEvent(domainEvent.Order.Id);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
