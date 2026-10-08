namespace manav.order.applicaiton.Features.CQRS.Commands.AddressComands;

public class CreateAddressCommand
{
    public string UserID { get; set; }
    public string AdressDetails { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
}