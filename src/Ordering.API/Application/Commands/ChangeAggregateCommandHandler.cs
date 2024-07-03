namespace EnShop.Ordering.API.Application.Commands;

public class ChangeAggregateCommandHandler : IRequestHandler<ChangeAggregateCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
    private readonly ILogger<ChangeAggregateCommandHandler> _logger;

    public ChangeAggregateCommandHandler(IMediator mediator,
        IOrderingIntegrationEventService orderingIntegrationEventService,
        IOrderRepository orderRepository,
        ILogger<ChangeAggregateCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(ChangeAggregateCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderId);
        if (orderToUpdate == null)
        {
            return false;
        }
        orderToUpdate.ChangeAggregate();

        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class ChangeAggregateIdentifiedCommandHandler : IdentifiedCommandHandler<ChangeAggregateCommand, bool>
{
    public ChangeAggregateIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<ChangeAggregateCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true;
    }
}
