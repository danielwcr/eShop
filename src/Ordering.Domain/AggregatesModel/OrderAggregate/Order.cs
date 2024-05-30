using System.ComponentModel.DataAnnotations;

namespace EnShop.Ordering.Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public string UserId { get; private set; }

    protected Order()
    {
    }

    public Order(string userId) : this()
    {
        UserId = userId;

        var domainEvent = new AggregateCreatedDomainEvent(this);
        this.AddDomainEvent(domainEvent);
    }

    public void ChangeAggregate()
    {
        AddDomainEvent(new AggregateChangedDomainEvent(this));
    }

    public void UpdateAggregate()
    {
        AddDomainEvent(new AggregateUpdatedDomainEvent(this));
    }
}
