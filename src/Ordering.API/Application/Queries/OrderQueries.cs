namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<IEnumerable<ListQueryDto>> ListQueryAsync(string userId)
    {
        return await context.Orders
            .Select(o => new ListQueryDto
            {
                id = o.Id,
                userId = o.UserId,
            })
            .ToListAsync();
    }
}
