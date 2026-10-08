using AutoMapper;
using manav.catalog.Dtos.AboutDtos;
using manav.catalog.Dtos.CategoryDtos;
using manav.catalog.Dtos.ContactDtos;
using manav.catalog.Dtos.GalleryDtos;
using manav.catalog.Dtos.HighlightsProductDtos;
using manav.catalog.Dtos.ProductDetailsDtos;
using manav.catalog.Dtos.ProductDtos;
using manav.catalog.Dtos.ProductImageDtos;
using manav.catalog.Entities;

namespace manav.catalog.Mapping;

//OUTOMAPPER: Sürekli nesne oluşturmayı kaldırıyor . Arkada outomapper yapar 

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, GetByIDcategoryDto>().ReverseMap();
        
        CreateMap<Product, ResultProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, GetByIdProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>().ReverseMap();
        
        CreateMap<ProductDetail, ResultProductDetailDto>().ReverseMap();
        CreateMap<ProductDetail, CreateProductDetailsDto>().ReverseMap();
        CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();
        CreateMap<ProductDetail, GetByIdProductDetailDto>().ReverseMap();
        
        CreateMap<ProductImages, ResultProductImageDto>().ReverseMap();
        CreateMap<ProductImages, CreateProductImageDto>().ReverseMap();
        CreateMap<ProductImages, UpdateProductImageDto>().ReverseMap();
        CreateMap<ProductImages, GetByIdProductImageDto>().ReverseMap();
        
        CreateMap<HighlightsProducts, ResultHighlightsProductDto>().ReverseMap();
        CreateMap<HighlightsProducts, CreateHighlightsProductDto>().ReverseMap();
        CreateMap<HighlightsProducts, UpdateHighlightsProductDto>().ReverseMap();
        CreateMap<HighlightsProducts, GetByIDHighlightsProductDto>().ReverseMap();
        CreateMap<HighlightsProducts, ResultHighlightsProductWithProductDto>().ReverseMap();

        CreateMap<About, ResultAboutDto>().ReverseMap();
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        
        CreateMap<Contact, ResultContactDto>().ReverseMap();
        CreateMap<Contact, CreateContactDto>().ReverseMap();
        CreateMap<Contact, GetByIdContactDto>().ReverseMap();
        
        CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
        CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
        CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
        CreateMap<Gallery, GetByIdGalleryDto>().ReverseMap();
    }
    
}