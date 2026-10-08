using AutoMapper;
using manav.catalog.Dtos.ContactDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.ContactServices;

public class ContactService:IContactService
{
    private readonly IMongoCollection<Contact> _contactCollection;
    private readonly IMapper _mapper;

    public ContactService(IMapper mapper, IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _contactCollection = database.GetCollection<Contact>(settings.ContactCollectionName);
        _mapper = mapper;
    }
    public async Task<List<ResultContactDto>> GetAllContactsAsync()
    {
        var values= await _contactCollection.Find(contact => true).ToListAsync();
        return _mapper.Map<List<ResultContactDto>>(values);
        
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        
        var value= _mapper.Map<Contact>(createContactDto);
        await _contactCollection.InsertOneAsync(value);
    }

    public async Task DeleteContactAsync(string id)
    {
        await _contactCollection.DeleteOneAsync(x=>x.ContactId == id);
    }

    public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
    {
        var value= await _contactCollection.Find(x=>x.ContactId==id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdContactDto>(value);
    }
}