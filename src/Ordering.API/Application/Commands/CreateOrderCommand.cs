namespace eShop.Ordering.API.Application.Commands;

[DataContract]
public class CreateOrderCommand
    : IRequest<bool>
{
    [DataMember]
    public string UserId { get; private set; }

    [DataMember]
    public string CardNumber { get; private set; }


    public CreateOrderCommand(string userId, string cardNumber)
    {
        UserId = userId;
        CardNumber = cardNumber;
    }
}
