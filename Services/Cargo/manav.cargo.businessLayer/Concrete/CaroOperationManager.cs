using manav.cargo.businessLayer.Abstract;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.businessLayer.Concrete;

public class CaroOperationManager:ICargoOperationService
{
    private readonly ICargoOperation _cargoOperation;

    public CaroOperationManager(ICargoOperation cargoOperation)
    {
        _cargoOperation = cargoOperation;
    }


    public void TInsert(CargoOperation entity)
    {
        _cargoOperation.Insert(entity);
    }

    public void TUpdate(CargoOperation entity)
    {
        _cargoOperation.Update(entity);
    }

    public void TDelete(int id)
    {
        _cargoOperation.Delete(id);
    }

    public CargoOperation TGetById(int id)
    {
        return _cargoOperation.GetById(id);
    }

    public List<CargoOperation> TGetAll()
    {
        return _cargoOperation.GetAll();
    }
}