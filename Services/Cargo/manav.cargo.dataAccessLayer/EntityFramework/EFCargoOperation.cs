using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.dataAccessLayer.Concrete;
using manav.cargo.dataAccessLayer.Repositories;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.dataAccessLayer.EntityFramework;

public class EFCargoOperation:GenericRepository<CargoOperation>,ICargoOperation
{
    public EFCargoOperation(CargoContext cargoContext) : base(cargoContext)
    {
    }
}