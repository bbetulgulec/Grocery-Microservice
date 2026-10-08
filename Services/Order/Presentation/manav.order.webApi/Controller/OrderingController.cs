using manav.order.applicaiton.Features.Mediator.Commands.OrderingCommands;
using manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.order.webApi.Controller;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderingController:ControllerBase
{
    private readonly IMediator _mediator;

    public OrderingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> OrderingList()
    {
        var values = await _mediator.Send(new GetOrderingQuery());
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrdering(int id)
    {
        var value = await _mediator.Send(new GetOrderingByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
    {
        int orderingId = await _mediator.Send(command);
        return Ok(orderingId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrdering(int id)
    {
        await _mediator.Send(new RemoveOrderingCommand(id));
        return Ok();
    }
    [HttpGet("GetOrderingByUserId/{id}")]
    public async Task<IActionResult> GetOrderingByUserId(string id)
    {
        var values = await _mediator.Send(new GetOrdiringByUserIdQuery(id));
        return Ok(values);
    }
    
}