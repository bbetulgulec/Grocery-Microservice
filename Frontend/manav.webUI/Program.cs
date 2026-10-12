using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using manav.webUI.Handlers;
using manav.webUI.Services;
using manav.webUI.Services.AwsS3Services;
using manav.webUI.Services.BasketServices;
using manav.webUI.Services.CatalogServices.AboutServices;
using manav.webUI.Services.CatalogServices.CategoryServices;
using manav.webUI.Services.CatalogServices.CommentServices;
using manav.webUI.Services.CatalogServices.ContactServices;
using manav.webUI.Services.CatalogServices.GalleryServices;
using manav.webUI.Services.CatalogServices.HighlightsProductServices;
using manav.webUI.Services.Concrete;
using manav.webUI.Services.Interfaces;
using manav.webUI.Services.MessageServices;
using manav.webUI.Services.OrderSevices.AddressServices;
using manav.webUI.Services.OrderSevices.OrderingServices;
using manav.webUI.Services.ProductServices;
using manav.webUI.Services.UserIdentityServices;
using manav.webUI.Settings;
using manav.webUI.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(
    JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Paces/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "ManavJwt";
    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromHours(5);
        options.Cookie.Name = "manavCookie";
        options.SlidingExpiration = true;
    });
builder.Services.AddAccessTokenManagement();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAboutValidator>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICloudStorageService, CloudStorageService>();
builder.Services.Configure<ClientSettings>(
    builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(
    builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddTransient<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialHandler>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
var awsSection = builder.Configuration.GetSection("AWS");
var awsOptions = new AWSOptions
{
    Region = RegionEndpoint.GetBySystemName(awsSection["Region"] ?? "eu-north-1")
};
if (!string.IsNullOrEmpty(awsSection["AccessKeyId"]))
    awsOptions.Credentials = new BasicAWSCredentials(
        awsSection["AccessKeyId"], awsSection["SecretAccessKey"]);
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
builder.Services.AddHttpClient<IUserService, UserService>(options =>
{
    options.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IHighlightsProductService, HighlightsProductService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IAboutService, AboutService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IContactService, ContactService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IGalleryService, GalleryService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<ICommentService, CommentService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IAddressService, AddressService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderingService, OrderingService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageService, MessageService>(options =>
{
    options.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(options =>
{
    options.BaseAddress = new Uri(
        $"{values.OcelotUrl}/{values.User.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

var supportedCulture = new[] { "en", "tr" };
var locaritationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCulture[1])
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

app.UseRequestLocalization(locaritationOptions);
// AREA ROUTE - default'tan ÖNCE olmalı
app.MapControllerRoute(
        "areas",
        "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
        "default",
        "{controller=Default}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();