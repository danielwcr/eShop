namespace EnShop.Ordering.UnitTests.Application;

public class IdentifiedCommandHandlerTest
{
    private readonly IRequestManager _requestManager;
    private readonly IMediator _mediator;
    private readonly ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> _loggerMock;

    public IdentifiedCommandHandlerTest()
    {
        _requestManager = Substitute.For<IRequestManager>();
        _mediator = Substitute.For<IMediator>();
        _loggerMock = Substitute.For<ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>>>();
    }

    [Fact]
    public async Task Handler_sends_command_when_order_no_exists()
    {
        // Arrange
        var fakeGuid = Guid.NewGuid();
        var fakeOrderCmd = new IdentifiedCommand<CreateOrderCommand, bool>(FakeOrderRequest(), fakeGuid);

        _requestManager.ExistAsync(Arg.Any<Guid>())
            .Returns(Task.FromResult(false));

        _mediator.Send(Arg.Any<IRequest<bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var handler = new CreateOrderIdentifiedCommandHandler(_mediator, _requestManager, _loggerMock);
        var result = await handler.Handle(fakeOrderCmd, CancellationToken.None);

        // Assert
        Assert.True(result);
        await _mediator.Received().Send(Arg.Any<IRequest<bool>>(), default);
    }

    [Fact]
    public async Task Handler_sends_no_command_when_order_already_exists()
    {
        // Arrange
        var fakeGuid = Guid.NewGuid();
        var fakeOrderCmd = new IdentifiedCommand<CreateOrderCommand, bool>(FakeOrderRequest(), fakeGuid);

        _requestManager.ExistAsync(Arg.Any<Guid>())
            .Returns(Task.FromResult(true));

        _mediator.Send(Arg.Any<IRequest<bool>>(), default)
            .Returns(Task.FromResult(true));

        // Act
        var handler = new CreateOrderIdentifiedCommandHandler(_mediator, _requestManager, _loggerMock);
        var result = await handler.Handle(fakeOrderCmd, CancellationToken.None);

        // Assert
        await _mediator.DidNotReceive().Send(Arg.Any<IRequest<bool>>(), default);
    }

    private CreateOrderCommand FakeOrderRequest(Dictionary<string, object> args = null)
    {
        return new CreateOrderCommand(
            userId: args != null && args.ContainsKey("userId") ? (string)args["userId"] : null,
            cardNumber: args != null && args.ContainsKey("cardNumber") ? (string)args["cardNumber"] : "1234"
           );
    }
}
