namespace EnShop.Ordering.API.Application.Commands;

// Regular CommandHandler
public class ChangeAggregateCommandHandler : IRequestHandler<ChangeAggregateCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public ChangeAggregateCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handler which processes the command when
    /// Stock service confirms the request
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(ChangeAggregateCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
        if (orderToUpdate == null)
        {
            return false;
        }

        orderToUpdate.ChangeAggregate();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


// Use for Idempotency in Command process
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
