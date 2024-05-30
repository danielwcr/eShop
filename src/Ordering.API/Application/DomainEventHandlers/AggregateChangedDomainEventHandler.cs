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
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.Order.Id, OrderStatus.StockConfirmed);

        var order = await _orderRepository.GetAsync(domainEvent.Order.Id);

        var integrationEvent = new AggregateChangedIntegrationEvent(order.Id, order.UserId);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
