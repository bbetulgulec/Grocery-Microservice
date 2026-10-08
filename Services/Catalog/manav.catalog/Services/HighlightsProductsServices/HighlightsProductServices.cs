using AutoMapper;
using manav.catalog.Dtos.HighlightsProductDtos;
using manav.catalog.Dtos.ProductDtos;
using manav.catalog.Entities;
using manav.catalog.Settings;
using MongoDB.Driver;

namespace manav.catalog.Services.HighlightsProductsServices;

public class HighlightsProductServices:IHighlightsProductServices
{
    private readonly IMongoCollection<HighlightsProducts> _highlightsProductsCollection;
    private readonly IMongoCollection<Product> _productsCollection; 
    private readonly IMapper _mapper;

    public HighlightsProductServices(IMapper mapper,IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _highlightsProductsCollection = database.GetCollection<HighlightsProducts>(settings.HighlightsProductCollectionName);
        _productsCollection = database.GetCollection<Product>(settings.ProductCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultHighlightsProductDto>> GetAllHighlightsProductAsync()
    {
        var values = await _highlightsProductsCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultHighlightsProductDto>>(values);    }

    

    public async Task CreateHighlightsProductAsync(CreateHighlightsProductDto createHighlightsProductDto)
    {
        var value = _mapper.Map<HighlightsProducts>(createHighlightsProductDto);
        value.ProductID = createHighlightsProductDto.Product?.ProductID;
        await _highlightsProductsCollection.InsertOneAsync(value);
    }

    public async Task UpdateHighlightsProductAsync(UpdateHighlightsProductDto updateHighlightsProductDto)
    {
        var value = _mapper.Map<HighlightsProducts>(updateHighlightsProductDto);
        value.ProductID = updateHighlightsProductDto.Product.ProductID;
        await _highlightsProductsCollection.FindOneAndReplaceAsync(x=>x.HighlightsProductsID == updateHighlightsProductDto.HighlightsProductsID, value);

    }

    public async Task DeleteHighlightsProductAsync(string id)
    {
        await _highlightsProductsCollection.DeleteOneAsync(x => x.HighlightsProductsID == id);
    }

    public async Task<GetByIDHighlightsProductDto> GetByIdHighlightsProductAsync(string id)
    {
        var values = await _highlightsProductsCollection.Find(x => x.HighlightsProductsID == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIDHighlightsProductDto>(values);  
    }

    public async Task<List<ResultHighlightsProductWithProductDto>> GetHighlightsProductListWithProductAsync()
    {
        var highlights = await _highlightsProductsCollection.Find(x => true).ToListAsync();
        var result = new List<ResultHighlightsProductWithProductDto>();

        foreach (var h in highlights)
        {
            var dto = _mapper.Map<ResultHighlightsProductWithProductDto>(h);

            if (!string.IsNullOrEmpty(h.ProductID))
            {
                var product = await _productsCollection.Find(p => p.ProductID == h.ProductID).FirstOrDefaultAsync();
                if (product != null)
                    dto.Product = _mapper.Map<ResultProductDto>(product);
            }

            result.Add(dto);
        }

        return result;
    }
}