namespace EnShop.Ordering.UnitTests.Domain;

using EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;
using EnShop.Ordering.UnitTests.Domain;

public class OrderAggregateTest
{
    public OrderAggregateTest()
    { }

    [Fact]
    public void Add_new_Order_raises_new_event()
    {
        var expectedResult = 1;

        var fakeOrder = new Order("1");

        Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }

    [Fact]
    public void Add_event_Order_explicitly_raises_new_event()
    {
        var expectedResult = 2;

        var fakeOrder = new Order("1");
        fakeOrder.AddDomainEvent(new AggregateCreatedDomainEvent(fakeOrder));

        Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }

    [Fact]
    public void Remove_event_Order_explicitly()
    {
        var fakeOrder = new Order("1");
        var fakeEvent = new AggregateCreatedDomainEvent(fakeOrder);
        var expectedResult = 1;

        fakeOrder.AddDomainEvent(fakeEvent);
        fakeOrder.RemoveDomainEvent(fakeEvent);

        Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }
}
