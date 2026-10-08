using manav.cargo.businessLayer.Abstract;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.businessLayer.Concrete;

public class CargoCompanyManager:ICargoCamponyService
{
    private readonly ICargoCompany _cargoCompany;

    public CargoCompanyManager(ICargoCompany company)
    {
        _cargoCompany = company;
    }
    public void TInsert(CargoCompany entity)
    {
        _cargoCompany.Insert(entity);
    }

    public void TUpdate(CargoCompany entity)
    {
        _cargoCompany.Update(entity);
    }

    public void TDelete(int id)
    {
        _cargoCompany.Delete(id);
    }

    public CargoCompany TGetById(int id)
    {
        return _cargoCompany.GetById(id);
    }

    public List<CargoCompany> TGetAll()
    {
        return _cargoCompany.GetAll();
    }
}