namespace EnShop.Ordering.API.Application.Commands;

[DataContract]
public class CreateAggregateCommand : IRequest<bool>
{
    [DataMember]
    public string UserId { get; private set; }

    public CreateAggregateCommand(string userId)
    {
        UserId = userId;
    }
}
