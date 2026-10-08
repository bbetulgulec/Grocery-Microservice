//Dapper da kod ve veri tabanı arasındaki bağlantıyı yapar entity framwork gibi daha hızlı ve küçük projelerde kullanılır 

using System.Data;
using Microsoft.Data.SqlClient;

namespace manav.discount.Context;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}