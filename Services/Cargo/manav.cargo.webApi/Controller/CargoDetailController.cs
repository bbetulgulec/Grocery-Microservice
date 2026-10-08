using manav.cargo.businessLayer.Abstract;
using manav.cargo.dtoLayer.Dtos.CargoDetailDtos;
using manav.cargo.entityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.cargo.webApi.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CargoDetailController:ControllerBase
{
    private readonly ICargoDetailService _cargoDetailService;

    public CargoDetailController(ICargoDetailService cargoDetailService)
    {
        _cargoDetailService = cargoDetailService;
    }

    [HttpGet]
    public IActionResult CargoDetailList()
    {
        var values= _cargoDetailService.TGetAll();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public IActionResult CargoDetail(int id)
    {
        var values = _cargoDetailService.TGetById(id);
        return Ok(values);
    }

    [HttpDelete("{id}")]
    public IActionResult CargoDetailDelete(int id)
    {
        _cargoDetailService.TDelete(id);
        return Ok();
    }

    [HttpPost]
    public IActionResult CargoDetailAdd(CreateCargoDetailDto cargoDetailDto)
    {
        CargoDetail cargoDetail= new CargoDetail
        {
            SenderCustomer = cargoDetailDto.SenderCustomer,
            ReceiverCustomer =  cargoDetailDto.ReceiverCustomer,
            Barcode =  cargoDetailDto.Barcode,
            CargoCompanyID =  cargoDetailDto.CargoCompanyID,
            
        };
        _cargoDetailService.TInsert( cargoDetail);
        return Ok();
    }

    [HttpPut]
    public IActionResult CargoDetailUpdate(UpdateCargoDetailDto cargoDetailDto)
    {
        CargoDetail cargoDetail= new CargoDetail
        {
            CargoDetailID = cargoDetailDto.CargoDetailID,
            SenderCustomer = cargoDetailDto.SenderCustomer,
            ReceiverCustomer =  cargoDetailDto.ReceiverCustomer,
            Barcode =  cargoDetailDto.Barcode,
            CargoCompanyID =  cargoDetailDto.CargoCompanyID,
            
        };
        _cargoDetailService.TUpdate( cargoDetail);
        return Ok();
    }
}
