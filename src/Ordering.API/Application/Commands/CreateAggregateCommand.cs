namespace EnShop.Ordering.API.Application.Commands;

[DataContract]
public class CreateAggregateCommand : IRequest<bool>
{
    [DataMember]
    public string UserId { get; private set; }

    [DataMember]
    public string CardNumber { get; private set; }


    public CreateAggregateCommand(string userId, string cardNumber)
    {
        UserId = userId;
        CardNumber = cardNumber;
    }
}
