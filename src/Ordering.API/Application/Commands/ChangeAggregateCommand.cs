namespace EnShop.Ordering.API.Application.Commands;

public record ChangeAggregateCommand(int OrderNumber) : IRequest<bool>;
