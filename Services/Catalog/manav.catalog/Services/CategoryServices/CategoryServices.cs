using AutoMapper;
using manav.catalog.Dtos.CategoryDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.CategoryServices;

public class CategoryServices:ICategoryServices
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMapper _mapper;

    public CategoryServices(IMapper mapper, IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);
        _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryCollection.Find(x => true).ToListAsync();
        var categoryDtos = _mapper.Map<List<ResultCategoryDto>>(categories);
        foreach (var item in categoryDtos)
        {
            var count = await _productCollection.CountDocumentsAsync(x => x.CategoryID == item.CategoryID);
            item.ProductCount = (int)count;
        }

        return categoryDtos;
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var value = _mapper.Map<Category>(createCategoryDto);
        await _categoryCollection.InsertOneAsync(value);
    }

    public  async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var value = _mapper.Map<Category>(updateCategoryDto);
        await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryID == updateCategoryDto.CategoryID, value);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id);
    }

    public async Task<GetByIDcategoryDto> GetByIdCategoryAsync(string id)
    {
        var values = await _categoryCollection.Find(x => x.CategoryID == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIDcategoryDto>(values);
        
    }
}