using manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;
using manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;
using manav.order.applicaiton.Features.CQRS.Queries.OrderDetailQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.order.webApi.Controller;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderDetailController:ControllerBase
{
    private readonly CreateOrderDetailsCommandHandler _createOrderDetailsCommandHandler;
    private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
    private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;
    private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
    private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;

    public OrderDetailController(CreateOrderDetailsCommandHandler createOrderDetailsCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler, GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler, GetOrderDetailQueryHandler getOrderDetailQueryHandler)
    {
        _createOrderDetailsCommandHandler = createOrderDetailsCommandHandler;
        _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
        _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
        _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
        _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetailList()
    {
        var values = await _getOrderDetailQueryHandler.Handle();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var value= await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailsCommand command)
    {
        await _createOrderDetailsCommandHandler.Handle(command);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailsCommand command)
    {
        await _updateOrderDetailCommandHandler.Handle(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailsCommand(id));
        return Ok();
    }
    
}