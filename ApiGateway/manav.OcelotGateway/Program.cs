using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration= new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration);

//Tokenın geçerliliğini sorgular
builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme",options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];//IdentityServer ile nirlikte ayağa kalkamyı sağlar
    options.Audience = "ResourceOcelot";//config tarafındaki dinleyici hangi keye sahipse ne yaapacağı işlemler velirlenir
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();
app.Run();

