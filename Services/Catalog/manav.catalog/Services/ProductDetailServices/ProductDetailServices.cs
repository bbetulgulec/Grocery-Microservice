using AutoMapper;
using FluentValidation;
using manav.catalog.Dtos.ProductDetailsDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.ProductDetailServices;

public class ProductDetailServices:IProductDetailServices
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductDetail> _productDetailCollection;
    private readonly IValidator<ProductDetail> _productDetailValidator;

    public ProductDetailServices(IMapper mapper, IDatabaseSettings settings, IValidator<ProductDetail> productDetailValidator)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _productDetailCollection = database.GetCollection<ProductDetail>(settings.ProductDetailCollectionName);
        _mapper = mapper;
        _productDetailValidator = productDetailValidator;
    }
    public async Task CreateProductDetailAsync(CreateProductDetailsDto productDetail)
    {
        var value= _mapper.Map<ProductDetail>(productDetail);
      await  _productDetailValidator.ValidateAndThrowAsync(value);
       await _productDetailCollection.InsertOneAsync(value);
    }

    public async Task UpdateProductDetailAsync(UpdateProductDetailDto productDetail)
    {
        var value = _mapper.Map<ProductDetail>(productDetail);
        await  _productDetailValidator.ValidateAndThrowAsync(value);
        await _productDetailCollection.FindOneAndReplaceAsync(x=>x.ProductDetailID == productDetail.ProductDetailID, value);

    }

    public async Task DeleteProductDetailAsync(string id)
    {
        await _productDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id);
    }

    public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
    {
        var value= await _productDetailCollection.Find(x => x.ProductDetailID == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdProductDetailDto>(value);
    }

    public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
    {
        var values= await _productDetailCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductDetailDto>>(values);
    }
}