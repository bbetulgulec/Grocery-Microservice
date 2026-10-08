using AutoMapper;
using manav.catalog.Dtos.ProductImageDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.ProductImageServices;

public class ProductImageServices:IProductImageServices
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductImages> _productImagesCollection;

    public ProductImageServices(IMapper mapper, IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _productImagesCollection = database.GetCollection<ProductImages>(settings.ProductImageCollectionName);
        _mapper = mapper;
    }
    
    public async Task CreateProductImageAsync(CreateProductImageDto productDetail)
    {
        var value=_mapper.Map<ProductImages>(productDetail);
        await _productImagesCollection.InsertOneAsync(value);
    }

    public async Task UpdateProductImageAsync(UpdateProductImageDto productDetail)
    {
        var value=_mapper.Map<ProductImages>(productDetail);
        await _productImagesCollection.FindOneAndReplaceAsync(x=>x.ProductImageID==productDetail.ProductImageID,value);
    }

    public async Task DeleteProductImageAsync(string id)
    {
        await _productImagesCollection.DeleteOneAsync(x=>x.ProductImageID==id);
    }

    public async Task<GetByIdProductImageDto> GetByIdPsroductImageAsync(string id)
    {
        var value=await _productImagesCollection.Find(x=>x.ProductImageID==id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdProductImageDto>(value);
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImagAsync()
    {
        var values=await _productImagesCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductImageDto>>(values);
    }
}