using AutoMapper;
using FluentValidation;
using manav.catalog.Dtos.AboutDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.AboutServices;

public class AboutServices:IAboutServices
{
    
    private readonly IMongoCollection<About> _aboutCollection;
    private readonly IMapper _mapper;
    private readonly IValidator<About> _aboutValidator;


    public AboutServices(IMapper mapper, IDatabaseSettings settings, IValidator<About> aboutValidator)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _aboutCollection = database.GetCollection<About>(settings.AboutCollectionName);
        _mapper = mapper;
        _aboutValidator = aboutValidator;
    }
    public async Task<ResultAboutDto>GetAboutAsync()
    {
        var values= await _aboutCollection.Find(x=>true).FirstOrDefaultAsync();
        return _mapper.Map<ResultAboutDto>(values);
    }

    public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
    {
        var value= _mapper.Map<About>(createAboutDto);
        await  _aboutValidator.ValidateAndThrowAsync(value);
        await _aboutCollection.InsertOneAsync(value);
    }

    public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
    {
        var value = _mapper.Map<About>(updateAboutDto);
        await  _aboutValidator.ValidateAndThrowAsync(value);
        await _aboutCollection.FindOneAndReplaceAsync(x=>x.AboutId == updateAboutDto.AboutId, value);
    }

    public async Task DeleteAboutAsync(string id)
    {
     await _aboutCollection.DeleteOneAsync(x => x.AboutId == id);
    }

}