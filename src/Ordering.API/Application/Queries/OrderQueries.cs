namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<IEnumerable<DetailsViewModel>> GetDetailsByFilterAsync(string filter)
    {
        return await context.Orders
            .Select(o => new DetailsViewModel
            {
                Id = o.Id,
            })
            .ToListAsync();
    }

    public async Task<DetailsViewModel> GetAggregateAsync(int id)
    {
        return await context.Orders
            .Select(o => new DetailsViewModel
            {
                Id = o.Id,
            })
            .FirstOrDefaultAsync();
    }
}
