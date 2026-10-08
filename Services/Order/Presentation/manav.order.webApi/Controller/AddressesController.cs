using manav.order.applicaiton.Features.CQRS.Commands.AddressComands;
using manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;
using manav.order.applicaiton.Features.CQRS.Queries.AddressQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.order.webApi.Controller;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AddressesController:ControllerBase
{
    private readonly GetAddressQueryHandler _getAddressQueryHandler;
    private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
    private readonly CreateAddressCommandHandler _createAddressCommandHandler;
    private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
    private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

    public AddressesController(GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler)
    {
        _getAddressQueryHandler = getAddressQueryHandler;
        _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
        _createAddressCommandHandler = createAddressCommandHandler;
        _updateAddressCommandHandler = updateAddressCommandHandler;
        _removeAddressCommandHandler = removeAddressCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> AddressList()
    {
        var values=await  _getAddressQueryHandler.Handle();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAddress(int id)
    {
        var value=await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
    {
       await _createAddressCommandHandler.Handle(command);
       return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
    {
        await _updateAddressCommandHandler.Handle(command);
        return Ok();
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAddress(int id)
    {
        await _removeAddressCommandHandler.Handle( new RemoveAddressCommand(id));
        return Ok();
    }
    
    
}