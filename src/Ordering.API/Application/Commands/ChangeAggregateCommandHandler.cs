namespace EnShop.Ordering.API.Application.Commands;

public class ChangeAggregateCommandHandler : IRequestHandler<ChangeAggregateCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public ChangeAggregateCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
        return true; // Ignore duplicate requests for processing order.
    }
}
