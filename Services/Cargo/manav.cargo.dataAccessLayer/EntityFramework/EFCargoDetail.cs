using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.dataAccessLayer.Concrete;
using manav.cargo.dataAccessLayer.Repositories;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.dataAccessLayer.EntityFramework;

public class EFCargoDetail:GenericRepository<CargoDetail>,ICargoDetail
{
    public EFCargoDetail(CargoContext cargoContext) : base(cargoContext)
    {
    }
}