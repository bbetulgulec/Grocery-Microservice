using manav.cargo.businessLayer.Abstract;
using manav.cargo.dtoLayer.Dtos.CargoCustomerDtos;
using manav.cargo.entityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.cargo.webApi.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CargoCustomerController : ControllerBase
{
    private readonly ICargoCustomerService _cargoCamponyService;

    public CargoCustomerController(ICargoCustomerService cargoCamponyService)
    {
        _cargoCamponyService = cargoCamponyService;
    }

    [HttpGet]
    public IActionResult CargoCustomerList()
    {
        var values = _cargoCamponyService.TGetAll();
        return Ok(values);
    }

    [HttpGet("id")]
    public IActionResult GetCargoCustomer(int id)
    {
        var value = _cargoCamponyService.TGetById(id);
        return Ok(value);
    }

    [HttpDelete]
    public IActionResult DeleteCargoCustomer(int id)
    {
        _cargoCamponyService.TDelete(id);
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateCargoCustomer(CreateCargoCustomerDto customerDto)
    {
        var cargoCustomer = new CargoCustomer
        {
            CargoCustomerAddress = customerDto.CargoCustomerAddress,
            CargoCustomerCity = customerDto.CargoCustomerCity,
            CargoCustomerDistrict = customerDto.CargoCustomerDistrict,
            CargoCustomerEmail = customerDto.CargoCustomerEmail,
            CargoCustomerName = customerDto.CargoCustomerName,
            CargoCustomerPhone = customerDto.CargoCustomerPhone,
            CargoCustomerSurname = customerDto.CargoCustomerSurname
        };
        _cargoCamponyService.TInsert(cargoCustomer);
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateCargoCustomer(UpdateCagoCustomerDto customerDto)
    {
        var cargoCustomer = new CargoCustomer
        {
            CargoCustomerID = customerDto.CargoCustomerID,
            CargoCustomerAddress = customerDto.CargoCustomerAddress,
            CargoCustomerCity = customerDto.CargoCustomerCity,
            CargoCustomerDistrict = customerDto.CargoCustomerDistrict,
            CargoCustomerEmail = customerDto.CargoCustomerEmail,
            CargoCustomerName = customerDto.CargoCustomerName,
            CargoCustomerPhone = customerDto.CargoCustomerPhone,
            CargoCustomerSurname = customerDto.CargoCustomerSurname
        };
        _cargoCamponyService.TUpdate(cargoCustomer);
        return Ok();
    }
}