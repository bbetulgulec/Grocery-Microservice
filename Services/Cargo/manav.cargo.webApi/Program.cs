using manav.cargo.businessLayer.Abstract;
using manav.cargo.businessLayer.Concrete;
using manav.cargo.dataAccessLayer.Abstract;
using manav.cargo.dataAccessLayer.Concrete;
using manav.cargo.dataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Tokenın geçerliliğini sorgular
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"]; //IdentityServer ile nirlikte ayağa kalkamyı sağlar
    options.Audience =
        "ResourceCargo"; //config tarafındaki dinleyici hangi keye sahipse ne yaapacağı işlemler velirlenir
    options.RequireHttpsMetadata = false;
});
builder.Services.AddDbContext<CargoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CargoDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ICargoCompany, EFCargoCompany>();
builder.Services.AddScoped<ICargoCamponyService, CargoCompanyManager>();
builder.Services.AddScoped<ICargoOperation, EFCargoOperation>();
builder.Services.AddScoped<ICargoOperationService, CaroOperationManager>();
builder.Services.AddScoped<ICargoCustomer, EFCargoCustomer>();
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();
builder.Services.AddScoped<ICargoDetail, EFCargoDetail>();
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();