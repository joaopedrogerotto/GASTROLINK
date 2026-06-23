using GastroLink.Client;
using GastroLink.Facade;
using GastroLink.Facade.Interface;
using GastroLink.Mappings;
using GastroLink.Service;
using GastroLink.Settings;
using Microsoft.Extensions.Options;
using System.Globalization;

var culture = new CultureInfo("pt-BR");

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiGastroLinkSettings>(builder.Configuration.GetSection("ApiGastroLink"));
builder.Services.AddHttpClient<LoginClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<MesaClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<TipoUsuarioClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<UsuarioClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<CategoriaPratoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<PratoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();
builder.Services.AddScoped<IFacadeMesa, FacadeMesa>();
builder.Services.AddScoped<IFacadeTipoUsuario, FacadeTipoUsuario>();
builder.Services.AddScoped<IFacadeCategoriaPrato, FacadeCategoriaPrato>();
builder.Services.AddScoped<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddScoped<IFacadePrato, FacadePrato>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
