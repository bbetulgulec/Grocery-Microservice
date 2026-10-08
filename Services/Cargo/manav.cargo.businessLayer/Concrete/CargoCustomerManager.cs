using manav.cargo.businessLayer.Abstract;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.businessLayer.Concrete;

public class CargoCustomerManager:ICargoCustomerService
{
    private readonly ICargoCustomer _cargoCustomer;

    public CargoCustomerManager(ICargoCustomer cargoCustomer)
    {
        _cargoCustomer = cargoCustomer;
    }

    public void TInsert(CargoCustomer entity)
    {
        _cargoCustomer.Insert(entity);
    }

    public void TUpdate(CargoCustomer entity)
    {
        _cargoCustomer.Update(entity);
    }

    public void TDelete(int id)
    {
        _cargoCustomer.Delete(id);
    }

    public CargoCustomer TGetById(int id)
    {
        return _cargoCustomer.GetById(id);
    }

    public List<CargoCustomer> TGetAll()
    {
        return _cargoCustomer.GetAll();
    }
}