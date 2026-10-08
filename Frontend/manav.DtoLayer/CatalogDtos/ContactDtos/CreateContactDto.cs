namespace manav.DtoLayer.CatalogDtos.ContactDtos;

public class CreateContactDto
{
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactSubject { get; set; }
    public string ContactMessage { get; set; }
    public bool IsRead { get; set; }
    protected DateTime SendDate { get; set; }
}