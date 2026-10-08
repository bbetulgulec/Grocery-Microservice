using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.dataAccessLayer.Concrete;
using manav.cargo.dataAccessLayer.Repositories;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.dataAccessLayer.EntityFramework;

public class EFCargoCompany:GenericRepository<CargoCompany>,ICargoCompany
{
    public EFCargoCompany(CargoContext cargoContext) : base(cargoContext)
    {
    }
}