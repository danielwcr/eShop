namespace EnShop.Ordering.UnitTests.Domain;

using global::Ordering.UnitTests.Builders;

public class OrderAggregateTest
{
    [Fact]
    public void ChangeAggregate_raises_AggregateChangedDomainEvent()
    {
        var order = OrderBuilder
            .For<SimpleOrder>()
            .Build();

        order.ChangeAggregate();

        Assert.Single(order.DomainEvents);
        Assert.Contains(new AggregateChangedDomainEvent(order), order.DomainEvents);
    }

    [Fact]
    public void ChangeAggregate_mutates_aggregate()
    {
        var order = OrderBuilder
            .For<SimpleOrder>()
            .Build();

        order.ChangeAggregate();

        Assert.Fail();
    }
}
