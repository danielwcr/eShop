﻿namespace EnShop.Ordering.API.Application.Commands;

using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

public class UpdateAggregateCommandHandler : IRequestHandler<CreateAggregateCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
    private readonly ILogger<UpdateAggregateCommandHandler> _logger;

    public UpdateAggregateCommandHandler(IMediator mediator,
        IOrderingIntegrationEventService orderingIntegrationEventService,
        IOrderRepository orderRepository,
        ILogger<UpdateAggregateCommandHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(CreateAggregateCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderId);
        if (orderToUpdate == null)
        {
            return false;
        }
        orderToUpdate.UpdateAggregate(command.UserId);

        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class UpdateAggregateIdentifiedCommandHandler : IdentifiedCommandHandler<CreateAggregateCommand, bool>
{
    public UpdateAggregateIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CreateAggregateCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true;
    }
}
