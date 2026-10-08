using manav.DtoLayer.OrderDtos.OrderingDtos;

namespace manav.webUI.Services.OrderSevices.OrderingServices;

public interface IOrderingService
{
    Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId);
    Task<int> CreateOrderingAsync(CreateOrderingDto createOrderingDto);
   Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto);
   Task<List<ResultOrderingByUserIdDto>> GetAllOrdersAsync();

}