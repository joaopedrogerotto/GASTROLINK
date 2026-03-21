using GastroLink.Facade;
using GastroLink.Facade.Interface;
using GastroLink.Service;
using GastroLink.Settings;
using Microsoft.Extensions.Options;

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


builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
