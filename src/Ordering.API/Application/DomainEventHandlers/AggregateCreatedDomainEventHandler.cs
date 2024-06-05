namespace EnShop.Ordering.API.Application.DomainEventHandlers;

public partial class AggregateCreatedDomainEventHandler
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
        var order = await _orderRepository.GetAsync(domainEvent.Order.Id);

        var integrationEvent = new AggregateCreatedIntegrationEvent(order.Id, order.UserId);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
