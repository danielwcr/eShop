namespace EnShop.Ordering.API.Application.DomainEventHandlers;

public class AggregateCreatedDomainEventHandler
                : INotificationHandler<AggregateCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public AggregateCreatedDomainEventHandler(
        IOrderRepository orderRepository,
        ILogger<AggregateCreatedDomainEventHandler> logger,
        IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    public async Task Handle(AggregateCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new AggregateCreatedIntegrationEvent(domainEvent.Order.Id);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
