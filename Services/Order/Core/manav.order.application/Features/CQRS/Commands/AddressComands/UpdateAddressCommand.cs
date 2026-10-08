namespace manav.order.applicaiton.Features.CQRS.Commands.AddressComands;

public class UpdateAddressCommand
{
    
    public int AdressID { get; set; }
    public String UserID { get; set; }
    public String AdressDetail { get; set; }

}