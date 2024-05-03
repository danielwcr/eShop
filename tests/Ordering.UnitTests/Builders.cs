using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace EnShop.Ordering.UnitTests.Domain;

public class OrderBuilder
{
    private readonly Order order;

    public OrderBuilder()
    {
        order = new Order(
            "userId",
            cardNumber: "12");
    }

    public Order Build()
    {
        return order;
    }
}
