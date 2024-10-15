namespace EnShop.Ordering.UnitTests.Application;

using Microsoft.AspNetCore.Http.HttpResults;
using EnShop.Ordering.API.Application.Queries;

public class OrdersWebApiTest
{
    private readonly IMediator _mediatorMock;
    private readonly IOrderQueries _orderQueriesMock;
    private readonly ILogger<OrderServices> _loggerMock;

    public OrdersWebApiTest()
    {
        _mediatorMock = Substitute.For<IMediator>();
        _orderQueriesMock = Substitute.For<IOrderQueries>();
        _loggerMock = Substitute.For<ILogger<OrderServices>>();
    }

    [Fact]
    public async Task Cancel_order_with_requestId_success()
    {
        // Arrange
        _mediatorMock.Send(Arg.Any<IdentifiedCommand<CreateAggregateCommand, bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.CreateAggregateAsync(Guid.NewGuid(), new CreateAggregateCommand(1, ""), orderServices);

        // Assert
        Assert.IsType<Ok>(result.Result);
    }

    [Fact]
    public async Task Cancel_order_bad_request()
    {
        // Arrange
        _mediatorMock.Send(Arg.Any<IdentifiedCommand<CreateAggregateCommand, bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.CreateAggregateAsync(Guid.Empty, new CreateAggregateCommand(1, ""), orderServices);

        // Assert
        Assert.IsType<BadRequest<string>>(result.Result);
    }

    [Fact]
    public async Task Get_orders_success()
    {
        // Arrange
        var fakeDynamicResult = Enumerable.Empty<DetailsViewModel>();

        _orderQueriesMock.GetCollectionByFilterAsync(Guid.NewGuid().ToString())
            .Returns(Task.FromResult(fakeDynamicResult));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.GetCollectionByFilterAsync("1", orderServices);

        // Assert
        Assert.IsType<Ok<IEnumerable<DetailsViewModel>>>(result);
    }
}
