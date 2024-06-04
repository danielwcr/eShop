namespace EnShop.Ordering.API.Application.Commands;

public record UpdateAggregateCommand(int OrderId) : IRequest<bool>;

