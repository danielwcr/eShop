namespace EnShop.Ordering.API.Infrastructure;

public class OrderingContextSeed: IDbSeeder<OrderingContext>
{
    public async Task SeedAsync(OrderingContext context)
    {
        await context.SaveChangesAsync();
    }
}
