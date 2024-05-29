namespace EnShop.Ordering.API.Application.Commands;

public record UpdateAgregateCommand(int OrderNumber) : IRequest<bool>;

