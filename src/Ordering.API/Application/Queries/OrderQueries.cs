namespace EnShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context) : IOrderQueries
{
    public async Task<GetQueryDto> GetQueryAsync(int id)
    {
        var order = await context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
            throw new KeyNotFoundException();

        return new GetQueryDto
        {
            ordernumber = order.Id,
        };
    }

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
