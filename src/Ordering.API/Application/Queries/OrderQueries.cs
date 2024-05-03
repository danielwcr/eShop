namespace eShop.Ordering.API.Application.Queries;

public class OrderQueries(OrderingContext context)
    : IOrderQueries
{
    public async Task<Order> GetOrderAsync(int id)
    {
        var order = await context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
      
        if (order is null)
            throw new KeyNotFoundException();

        return new Order
        {
            ordernumber = order.Id,
            date = order.OrderDate,
            description = order.Description,
            status = order.OrderStatus.ToString()
        };
    }

    public async Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(string userId)
    {
        return await context.Orders
            .Where(o => o.UserId == userId)  
            .Select(o => new OrderSummary
            {
                ordernumber = o.Id,
                date = o.OrderDate,
                status = o.OrderStatus.ToString(),
            })
            .ToListAsync();
    } 
    
}
