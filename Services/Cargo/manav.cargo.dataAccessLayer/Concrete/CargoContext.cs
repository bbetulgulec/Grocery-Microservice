using manav.cargo.entityLayer.Concrate;
using Microsoft.EntityFrameworkCore;

namespace manav.cargo.dataAccessLayer.Concrete;

public class CargoContext : DbContext
{
    public CargoContext(DbContextOptions<CargoContext> options)
        : base(options)
    {
    }

    public DbSet<CargoCompany> CargoCompany { get; set; }
    public DbSet<CargoCustomer> CargoCustomer { get; set; }
    public DbSet<CargoDetail> CargoDetail { get; set; }
    public DbSet<CargoOperation> CargoOperation { get; set; }
}