using Dapper;
using manav.discount.Context;
using manav.discount.Dtos;

namespace manav.discount.Services;

public class DiscountServices:IDiscountServices
{
    private readonly DapperContext _dapperContext;

    public DiscountServices(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<ResultCouponDto>> GetAllCouponAsync()
    {
        string query = "select * from Coupons";
        using (var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultCouponDto>(query);
            return values.ToList();
        }
    }

    public async Task CreateCouponAsync(CreateCouponDto dto)
    {
        string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values(@Code,@Rate,@IsActive,@ValidDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@Code", dto.Code);
        parameters.Add("@Rate", dto.Rate);
        parameters.Add("@IsActive", dto.IsActive);
        parameters.Add("@ValidDate", dto.ValidDate);
        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task UpdateCouponAsync(UpdateCouponDto dto)
    {
        string query = "Update Coupons Set Code=@Code,Rate=@Rate,IsActive=@IsActive,ValidDate=@ValidDate Where CouponID=@CouponID";
        var parameters = new DynamicParameters();
        parameters.Add("@CouponID", dto.CouponID);
        parameters.Add("@Code", dto.Code);
        parameters.Add("@Rate", dto.Rate);
        parameters.Add("@IsActive", dto.IsActive);
        parameters.Add("@ValidDate", dto.ValidDate);
        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteCouponAsync(string id)
    {
        string query = "delete from Coupons where CouponID=@CouponID";
        var parameters = new DynamicParameters();
        parameters.Add("@CouponID", id);
        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<GetByIdCouponDto> GetByIdCouponAsync(string id)
    {
        string query = "Select * From Coupons Where CouponID =@CouponID";
        var parameters = new DynamicParameters();
        parameters.Add("@CouponID", id);
        using (var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query, parameters);
            return values;
        }
    }
}