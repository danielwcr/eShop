namespace EnShop.Ordering.UnitTests.Application;

using Microsoft.AspNetCore.Http.HttpResults;
using EnShop.Ordering.API.Application.Queries;
using GetQueryDto = EnShop.Ordering.API.Application.Queries.GetQueryDto;
using NSubstitute.ExceptionExtensions;

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
        _mediatorMock.Send(Arg.Any<IdentifiedCommand<UpdateAggregateCommand, bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.UpdateAggregateAsync(Guid.NewGuid(), new UpdateAggregateCommand(1), orderServices);

        // Assert
        Assert.IsType<Ok>(result.Result);
    }

    [Fact]
    public async Task Cancel_order_bad_request()
    {
        // Arrange
        _mediatorMock.Send(Arg.Any<IdentifiedCommand<UpdateAggregateCommand, bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.UpdateAggregateAsync(Guid.Empty, new UpdateAggregateCommand(1), orderServices);

        // Assert
        Assert.IsType<BadRequest<string>>(result.Result);
    }

    [Fact]
    public async Task Get_orders_success()
    {
        // Arrange
        var fakeDynamicResult = Enumerable.Empty<ListQueryDto>();

        _orderQueriesMock.ListQueryAsync(Guid.NewGuid().ToString())
            .Returns(Task.FromResult(fakeDynamicResult));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.ListQueryAsync("1", orderServices);

        // Assert
        Assert.IsType<Ok<IEnumerable<ListQueryDto>>>(result);
    }

    [Fact]
    public async Task Get_order_success()
    {
        // Arrange
        var fakeOrderId = 123;
        var fakeDynamicResult = new GetQueryDto();
        _orderQueriesMock.GetQueryAsync(Arg.Any<int>())
            .Returns(Task.FromResult(fakeDynamicResult));

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.GetQueryAsync(fakeOrderId, orderServices);

        // Assert
        var okResult = Assert.IsType<Ok<GetQueryDto>>(result.Result);
        Assert.Same(fakeDynamicResult, okResult.Value);
    }

    [Fact]
    public async Task Get_order_fails()
    {
        // Arrange
        var fakeOrderId = 123;
#pragma warning disable NS5003
        _orderQueriesMock.GetQueryAsync(Arg.Any<int>())
            .Throws(new KeyNotFoundException());
#pragma warning restore NS5003

        // Act
        var orderServices = new OrderServices(_mediatorMock, _orderQueriesMock, _loggerMock);
        var result = await OrdersApi.GetQueryAsync(fakeOrderId, orderServices);

        // Assert
        Assert.IsType<NotFound>(result.Result);
    }
}
