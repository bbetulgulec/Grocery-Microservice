using manav.cargo.businessLayer.Abstract;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.dtoLayer.Dtos.CargoDetailDtos;
using manav.cargo.dtoLayer.Dtos.CargoOperationDtos;
using manav.cargo.entityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.cargo.webApi.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CargoOperationController:ControllerBase
{
    private readonly ICargoOperationService _cargoOperationService;

    public CargoOperationController(ICargoOperationService cargoOperationService)
    {
        _cargoOperationService = cargoOperationService;
    }
    [HttpGet]
    public IActionResult CargoOperationList()
    {
        var values= _cargoOperationService.TGetAll();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public IActionResult CargoOperation(int id)
    {
        var values = _cargoOperationService.TGetById(id);
        return Ok(values);
    }

    [HttpDelete("{id}")]
    public IActionResult CargoDetailDelete(int id)
    {
        _cargoOperationService.TDelete(id);
        return Ok();
    }

    [HttpPost]
    public IActionResult CargoDetailAdd(CrateCargoOperationDto cargoOperationDto)
    {
        CargoOperation cargoOperation= new CargoOperation
        {
            Barcode =  cargoOperationDto.Barcode,
            Desciription = cargoOperationDto.Desciription,
            OperationDate =  cargoOperationDto.OperationDate,
            
        };
        _cargoOperationService.TInsert( cargoOperation);
        return Ok();
    }

    [HttpPut]
    public IActionResult CargoOperationUpdate(UpdateCargoOperationDto cargoOperationDto)
    {
        CargoOperation cargoOperation= new CargoOperation
        {
            CargoOperationID =  cargoOperationDto.CargoOperationID,
            Barcode =  cargoOperationDto.Barcode,
            Desciription = cargoOperationDto.Desciription,
            OperationDate =  cargoOperationDto.OperationDate,
            
        };
        _cargoOperationService.TUpdate( cargoOperation);
        return Ok();
    }
}