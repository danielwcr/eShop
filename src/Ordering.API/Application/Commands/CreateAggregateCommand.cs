namespace EnShop.Ordering.API.Application.Commands;

[DataContract]
public class CreateAggregateCommand : IRequest<bool>
{
    [DataMember]
    public int OrderId { get; }

    [DataMember]
    public string UserId { get; private set; }

    public CreateAggregateCommand(int orderId, string userId)
    {
        OrderId = orderId;
        UserId = userId;
    }
}
