namespace EnShop.Ordering.API.Application.Commands;

public record CreateAggregateCommand(int OrderId, string UserId) : IRequest<bool>;
