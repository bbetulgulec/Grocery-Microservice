namespace manav.order.applicaiton.Features.CQRS.Commands.AddressComands;

public class RemoveAddressCommand
{
    public int Id { get; set; }

    public RemoveAddressCommand(int id)
    {
        Id = id;
    }


}