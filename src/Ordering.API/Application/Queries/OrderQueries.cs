namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<IEnumerable<ListQueryDto>> ListQueryAsync(string userId)
    {
        return await context.Orders
            .Where(o => o.UserId == userId)
            .Select(o => new ListQueryDto
            {
                ordernumber = o.Id,
            })
            .ToListAsync();
    }
}
