namespace EnShop.Ordering.API.Application.Commands;

public record UpdateAggregateCommand(int OrderNumber) : IRequest<bool>;

