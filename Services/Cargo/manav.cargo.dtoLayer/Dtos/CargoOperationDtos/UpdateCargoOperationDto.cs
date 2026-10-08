namespace manav.cargo.dtoLayer.Dtos.CargoOperationDtos;

public class UpdateCargoOperationDto
{
    public int CargoOperationID { get; set; }
    public string Barcode { get; set; }
    public string Desciription { get; set; }
    public DateTime OperationDate { get; set; }
}