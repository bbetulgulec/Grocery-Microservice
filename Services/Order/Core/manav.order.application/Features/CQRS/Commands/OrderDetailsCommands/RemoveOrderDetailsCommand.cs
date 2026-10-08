namespace manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;

public class RemoveOrderDetailsCommand
{
    public int  Id { get; set; }

    public RemoveOrderDetailsCommand(int id)
    {
        Id = id;
    }
}