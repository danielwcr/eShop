public class OrderServices(
    IMediator mediator,
    IOrderQueries queries,
    ILogger<OrderServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<OrderServices> Logger { get; } = logger;
    public IOrderQueries Queries { get; } = queries;
}
