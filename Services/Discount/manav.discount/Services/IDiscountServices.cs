using manav.discount.Dtos;

namespace manav.discount.Services;

public interface IDiscountServices
{
    Task<List<ResultCouponDto>> GetAllCouponAsync();
    Task CreateCouponAsync(CreateCouponDto dto);
    Task UpdateCouponAsync(UpdateCouponDto dto);
    Task DeleteCouponAsync(string id);
    Task<GetByIdCouponDto> GetByIdCouponAsync(string id);
}