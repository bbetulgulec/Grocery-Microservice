using manav.cargo.businessLayer.Abstract;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.entityLayer.Concrate;

namespace manav.cargo.businessLayer.Concrete;

public class CargoDetailManager:ICargoDetailService
{
    private readonly ICargoDetail _cargoDetail;

    public CargoDetailManager(ICargoDetail cargoDetail)
    {
        _cargoDetail = cargoDetail;
    }

    public void TInsert(CargoDetail entity)
    {
        _cargoDetail.Insert(entity);
    }

    public void TUpdate(CargoDetail entity)
    {
        _cargoDetail.Update(entity);
    }

    public void TDelete(int id)
    {
        _cargoDetail.Delete(id);
    }

    public CargoDetail TGetById(int id)
    {
        return _cargoDetail.GetById(id);
    }

    public List<CargoDetail> TGetAll()
    {
        return _cargoDetail.GetAll();
    }
}