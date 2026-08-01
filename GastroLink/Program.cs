using GastroLink.Client;
using GastroLink.Facade;
using GastroLink.Facade.Interface;
using GastroLink.Handlers;
using GastroLink.Service;
using GastroLink.Service.Interfaces;
using GastroLink.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Globalization;

var culture = new CultureInfo("pt-BR");

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtAuthHandler>();

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
}).AddHttpMessageHandler<JwtAuthHandler>();

builder.Services.AddHttpClient<TipoUsuarioClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();

builder.Services.AddHttpClient<UsuarioClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();

builder.Services.AddHttpClient<CategoriaPratoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();

builder.Services.AddHttpClient<PratoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<CardapioClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<PedidoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<CozinhaClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<GarcomClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<CaixaClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<FormaPagamentoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();
builder.Services.AddHttpClient<PagamentoClient>((name, client) => {
    var settings = name
        .GetRequiredService<IOptions<ApiGastroLinkSettings>>()
        .Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
}).AddHttpMessageHandler<JwtAuthHandler>();




builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();
builder.Services.AddScoped<IFacadeCardapio, FacadeCardapio>();
builder.Services.AddScoped<IFacadeMesa, FacadeMesa>();
builder.Services.AddScoped<IFacadeTipoUsuario, FacadeTipoUsuario>();
builder.Services.AddScoped<IFacadeCategoriaPrato, FacadeCategoriaPrato>();
builder.Services.AddScoped<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddScoped<IFacadePrato, FacadePrato>();
builder.Services.AddScoped<IFacadePedido, FacadePedido>();
builder.Services.AddScoped<IFacadePagamento, FacadePagamento>();

builder.Services.AddScoped<IRascunhoPedidoService, RascunhoPedidoService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Sair";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.Events.OnRedirectToLogin = context => {
            context.HttpContext.Response.Redirect($"{context.RedirectUri}");

            var tempdata = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>().GetTempData(context.HttpContext);
            tempdata["FalhaLogin"] = "É necessário fazer login para acessar o sistema.";
            tempdata.Save();

            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets().AllowAnonymous();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
