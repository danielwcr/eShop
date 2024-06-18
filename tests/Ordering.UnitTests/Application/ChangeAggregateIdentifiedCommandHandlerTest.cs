using EnShop.Ordering.API.Application.IntegrationEvents;
using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.UnitTests.Builders;

namespace EnShop.Ordering.UnitTests.Application;

public class ChangeAggregateIdentifiedCommandHandlerTest
{
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<ChangeAggregateCommand, bool>> _logger;
    private readonly ChangeAggregateIdentifiedCommandHandler _handler;
    private readonly CancellationTokenSource _cts;

    public ChangeAggregateIdentifiedCommandHandlerTest()
    {
        _cts = new CancellationTokenSource();
        _mediator = Substitute.For<IMediator>();
        _requestManager = Substitute.For<IRequestManager>();
        _logger = Substitute.For<ILogger<IdentifiedCommandHandler<ChangeAggregateCommand, bool>>>();
        _handler = new ChangeAggregateIdentifiedCommandHandler(_mediator, _requestManager, _logger);
    }

    [Fact]
    public async Task Ignore_duplicate_requests()
    {
        var aggregate = GivenAggregate<SimpleOrder>();
        var command = GivenCommand(Guid.NewGuid(), aggregate.Id);

        _requestManager
            .ExistAsync(command.Id)
            .Returns(Task.FromResult(true));

        var result = await _handler.Handle(command, _cts.Token);

        Assert.True(result);
    }

    private Order GivenAggregate<T>(Action<OrderBuilder> setup = null) where T : WellKnownOrder, new()
    {
        var builder = OrderBuilder.For<T>();
        setup?.Invoke(builder);
        return builder.Build();
    }

    private IdentifiedCommand<ChangeAggregateCommand, bool> GivenCommand(Guid id, int orderId)
    {
        var command = new ChangeAggregateCommand(orderId);
        return new IdentifiedCommand<ChangeAggregateCommand, bool>(command, id);
    }
}
