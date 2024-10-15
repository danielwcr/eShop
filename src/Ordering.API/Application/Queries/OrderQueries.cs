namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<IEnumerable<DetailsViewModel>> GetCollectionByFilterAsync(string filter)
    {
        return await context.Orders
            .Select(o => new DetailsViewModel
            {
                Id = o.Id,
            })
            .ToListAsync();
    }

    public async Task<DetailsViewModel> GetSingleObjectByFilterAsync(string filter)
    {
        return await context.Orders
            .Select(o => new DetailsViewModel
            {
                Id = o.Id,
            })
            .FirstOrDefaultAsync();
    }
}
