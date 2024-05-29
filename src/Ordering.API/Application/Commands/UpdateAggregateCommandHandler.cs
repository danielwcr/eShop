namespace EnShop.Ordering.API.Application.Commands;

// Regular CommandHandler
public class UpdateAggregateCommandHandler : IRequestHandler<UpdateAgregateCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateAggregateCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handler which processes the command when
    /// customer executes cancel order from app
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateAgregateCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
        if (orderToUpdate == null)
        {
            return false;
        }

        orderToUpdate.UpdateAggregate();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


// Use for Idempotency in Command process
public class UpdateCommandIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateAgregateCommand, bool>
{
    public UpdateCommandIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<UpdateAgregateCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true; // Ignore duplicate requests for processing order.
    }
}
