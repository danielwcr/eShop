namespace EnShop.Ordering.API.Application.Commands;

public record ChangeAggregateCommand(int OrderId, string UserId) : IRequest<bool>;
