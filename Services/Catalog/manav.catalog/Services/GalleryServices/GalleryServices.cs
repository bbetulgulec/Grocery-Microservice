using AutoMapper;
using manav.catalog.Dtos.GalleryDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.GalleryServices;

public class GalleryServices:IGalleryServices
{
    private readonly IMongoCollection<Gallery> _galleryCollection;
    private readonly IMapper _mapper;
    public GalleryServices(IMapper mapper, IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _galleryCollection = database.GetCollection<Gallery>(settings.GalleryCollectionName);
        _mapper = mapper;
    }
    
    public async Task<List<ResultGalleryDto>> GetAllGalleryAsync()
    {
        var values= await _galleryCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultGalleryDto>>(values);
    }

    public async Task CreateGalleryAsync(CreateGalleryDto createGalleryDto)
    {
        var value = _mapper.Map<Gallery>(createGalleryDto);
        await _galleryCollection.InsertOneAsync(value);
    }

    public async Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto)
    {
        var value = _mapper.Map<Gallery>(updateGalleryDto);
        await _galleryCollection.FindOneAndReplaceAsync(x=>x.GallaryId == updateGalleryDto.GallaryId, value);
    }

    public async Task DeleteGalleryAsync(string id)
    {
        await _galleryCollection.DeleteOneAsync(x=>x.GallaryId == id);
    }

    public async Task<GetByIdGalleryDto> GetByIdGalleryAsync(string id)
    {
        var values = await _galleryCollection.Find(x => x.GallaryId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdGalleryDto>(values);
    }
}