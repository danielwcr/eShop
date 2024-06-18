using EnShop.Ordering.API.Application.IntegrationEvents;
using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.UnitTests.Builders;

namespace EnShop.Ordering.UnitTests.Application;

public class ChangeAggregateCommandHandlerTest
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<ChangeAggregateCommandHandler> _logger;
    private readonly ChangeAggregateCommandHandler _handler;
    private readonly CancellationTokenSource _cts;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public ChangeAggregateCommandHandlerTest()
    {
        _cts = new CancellationTokenSource();
        _orderRepository = Substitute.For<IOrderRepository>();
        _orderingIntegrationEventService = Substitute.For<IOrderingIntegrationEventService>();
        _mediator = Substitute.For<IMediator>();
        _logger = Substitute.For<ILogger<ChangeAggregateCommandHandler>>();
        _handler = new ChangeAggregateCommandHandler(_mediator, _orderingIntegrationEventService, _orderRepository, _logger);
    }

    [Fact]
    public async Task Return_true_when_aggregate_is_persisted()
    {
        var aggregate = GivenAggregate<SimpleOrder>();
        var command = GivenCommand(aggregate.Id);

        _orderRepository
            .GetAsync(aggregate.Id)
            .Returns(Task.FromResult(aggregate));

        _orderRepository.UnitOfWork.SaveEntitiesAsync(_cts.Token)
            .Returns(Task.FromResult(true));

        var result = await _handler.Handle(command, _cts.Token);

        Assert.True(result);
        Assert.NotEmpty(aggregate.DomainEvents);
    }

    [Fact]
    public async Task Return_false_when_aggregate_is_not_persisted()
    {
        var aggregate = GivenAggregate<SimpleOrder>();
        var command = GivenCommand(aggregate.Id);

        _orderRepository
            .GetAsync(aggregate.Id)
            .Returns(Task.FromResult(aggregate));

        _orderRepository.UnitOfWork.SaveEntitiesAsync(_cts.Token)
            .Returns(Task.FromResult(false));

        var result = await _handler.Handle(command, _cts.Token);

        Assert.False(result);
    }

    [Fact]
    public async Task Return_false_when_aggregate_is_not_found()
    {
        var command = GivenCommand(123);

        var result = await _handler.Handle(command, _cts.Token);

        Assert.False(result);
    }

    private Order GivenAggregate<T>(Action<OrderBuilder> setup = null) where T : WellKnownOrder, new()
    {
        var builder = OrderBuilder.For<T>();
        setup?.Invoke(builder);
        return builder.Build();
    }

    private ChangeAggregateCommand GivenCommand(int orderId)
    {
        return new ChangeAggregateCommand(orderId);
    }
}
