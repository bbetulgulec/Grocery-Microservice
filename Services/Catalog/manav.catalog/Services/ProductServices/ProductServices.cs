using AutoMapper;
using FluentValidation;
using manav.catalog.Dtos.ProductDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.ProductServices;

public class ProductServices:IProductServices
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _productCategoryCollection;
    private readonly IValidator<Product> _productValidator;

    public ProductServices(IMapper mapper, IDatabaseSettings settings, IValidator<Product> productValidator)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
        _mapper = mapper;
        _productValidator = productValidator;
        _productCategoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);
    }
    public async Task<List<ResultProductDto>> GetAllCategoriesAsync()
    {
        var values = await _productCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductDto>>(values);
    }

    public async Task<UpdateProductDto> GetByIdProductAsync(string id)
    {
        var value= await _productCollection.Find(x=>x.ProductID==id).FirstOrDefaultAsync();
        return _mapper.Map<UpdateProductDto>(value);
    }

    public Task UpdateProductDto(UpdateProductDto updateProductDto)
    {
        var value= _mapper.Map<Product>(updateProductDto);
        _productValidator.ValidateAndThrowAsync(value);
        return _productCollection.ReplaceOneAsync(x=>x.ProductID == updateProductDto.ProductID, value);
    }

    public Task CreateProductDto(CreateProductDto createProductDto)
    {
        var value= _mapper.Map<Product>(createProductDto);
         _productValidator.ValidateAndThrowAsync(value);
        return _productCollection.InsertOneAsync(value);
    }

    public Task DeleteProductAsync(string id)
    {
        return _productCollection.DeleteOneAsync(x => x.ProductID == id);
    }

    public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
    {
        var values= await _productCollection.Find(x => true).ToListAsync();
        foreach (var items in values)
        {
            items.Category= await _productCategoryCollection.Find<Category>(x=>x.CategoryID == items.CategoryID).FirstOrDefaultAsync();
        }
        return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
    }

    public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
    {
        var values= await _productCollection.Find(x => x.CategoryID==categoryId).ToListAsync();
        foreach (var items in values)
        {
            items.Category= await _productCategoryCollection.Find<Category>(x=>x.CategoryID == items.CategoryID).FirstOrDefaultAsync();
        }
        return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
    }
}