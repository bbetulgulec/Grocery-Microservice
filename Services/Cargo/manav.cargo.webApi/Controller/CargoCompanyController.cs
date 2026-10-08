using manav.cargo.businessLayer.Abstract;
using manav.cargo.dtoLayer.Dtos;
using manav.cargo.entityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.cargo.webApi.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CargoCompanyController : ControllerBase
{
    private readonly ICargoCamponyService _cargoCamponyService;

    public CargoCompanyController(ICargoCamponyService cargoCamponyService)
    {
        _cargoCamponyService = cargoCamponyService;
    }

    [HttpGet]
    public IActionResult CargoCompanyList()
    {
        var values = _cargoCamponyService.TGetAll();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public IActionResult CargoCompanyGet(int id)
    {
        var values = _cargoCamponyService.TGetById(id);
        return Ok(values);
    }

    [HttpPost]
    public IActionResult CargoCompanyAdd(CreateCargoCompanyDto createCargoCompanyDto)
    {
        var cargoCompany = new CargoCompany
        {
            CargoCompanyName = createCargoCompanyDto.CargoCompanyName
        };
        _cargoCamponyService.TInsert(cargoCompany);
        return Ok("Cargo Company Added");
    }

    [HttpDelete]
    public IActionResult CargoCompanyDelete(int id)
    {
        _cargoCamponyService.TDelete(id);
        return Ok();
    }

    [HttpPut]
    public IActionResult CargoCompanyUpdate(UpdateCargoCompanyDto updateCargoCompanyDto)
    {
        var cargoCompany = new CargoCompany
        {
            CargoCompanyId = updateCargoCompanyDto.CargoCompanyId,
            CargoCompanyName = updateCargoCompanyDto.CargoCompanyName
        };
        _cargoCamponyService.TUpdate(cargoCompany);
        return Ok();
    }
}