using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Commands.OrderingCommands;

public class CreateOrderingCommand:IRequest<int>
{
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}