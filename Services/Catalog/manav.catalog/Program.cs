using System.Reflection;
using FluentValidation;
using manav.catalog.Entities;
using manav.catalog.Services.AboutServices;
using manav.catalog.Services.CategoryServices;
using manav.catalog.Services.ContactServices;
using manav.catalog.Services.GalleryServices;
using manav.catalog.Services.HighlightsProductsServices;
using manav.catalog.Services.ProductDetailServices;
using manav.catalog.Services.ProductImageServices;
using manav.catalog.Services.ProductServices;
using manav.catalog.Settings;
using manav.catalog.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Tokenın geçerliliğini sorgular
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];//IdentityServer ile nirlikte ayağa kalkamyı sağlar
    options.Audience = "ResourceCatalog";//config tarafındaki dinleyici hangi keye sahipse ne yaapacağı işlemler velirlenir
    options.RequireHttpsMetadata = false;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductImageServices, ProductImageServices>();
builder.Services.AddScoped<IProductDetailServices, ProductDetailServices>();
builder.Services.AddScoped<IHighlightsProductServices, HighlightsProductServices>();
builder.Services.AddScoped<IAboutServices, AboutServices>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IGalleryServices, GalleryServices>();





builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<ProductDetail>, ProductDetailValidator>();
builder.Services.AddScoped<IValidator<About>, AboutValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();  
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

