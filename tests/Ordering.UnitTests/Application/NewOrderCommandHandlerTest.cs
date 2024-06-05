using EnShop.Ordering.API.Application.IntegrationEvents;
using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace EnShop.Ordering.UnitTests.Application;

public class NewOrderRequestHandlerTest
{
    private readonly IOrderRepository _orderRepositoryMock;
    private readonly IMediator _mediator;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public NewOrderRequestHandlerTest()
    {

        _orderRepositoryMock = Substitute.For<IOrderRepository>();
        _orderingIntegrationEventService = Substitute.For<IOrderingIntegrationEventService>();
        _mediator = Substitute.For<IMediator>();
    }

    [Fact]
    public async Task Handle_return_false_if_order_is_not_persisted()
    {
        var fakeOrderCmd = FakeOrderRequestWithBuyer();

        _orderRepositoryMock.GetAsync(Arg.Any<int>())
            .Returns(Task.FromResult(FakeOrder()));

        _orderRepositoryMock.UnitOfWork.SaveChangesAsync(default)
            .Returns(Task.FromResult(1));

        var LoggerMock = Substitute.For<ILogger<CreateAggregateCommandHandler>>();
        //Act
        var handler = new CreateAggregateCommandHandler(_mediator, _orderingIntegrationEventService, _orderRepositoryMock, LoggerMock);
        var cltToken = new CancellationToken();
        var result = await handler.Handle(fakeOrderCmd, cltToken);

        //Assert
        Assert.False(result);
    }

    private Order FakeOrder()
    {
        return new Order("1");
    }

    private CreateAggregateCommand FakeOrderRequestWithBuyer(Dictionary<string, object> args = null)
    {
        return new CreateAggregateCommand(
            0,
            UserId: args != null && args.ContainsKey("userId") ? (string)args["userId"] : null
            );
    }
}
