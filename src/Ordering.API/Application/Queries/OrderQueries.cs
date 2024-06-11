namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<IEnumerable<Details>> GetDetailsByFilterAsync(string filter)
    {
        return await context.Orders
            .Select(o => new Details
            {
                Id = o.Id,
                UserId = o.UserId,
            })
            .ToListAsync();
    }
}
