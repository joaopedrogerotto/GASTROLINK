using APIGastroLink.DAO;
using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Factory;
using APIGastroLink.Factory.Interfaces;
using APIGastroLink.Hubs;
using APIGastroLink.Services;
using APIGastroLink.Services.Interfaces;
using APIGastroLink.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDAODatabase, DAOSQLServer>();
builder.Services.AddScoped<IDAOLogin, DAOLogin>();
builder.Services.AddScoped<IDAOMesa, DAOMesa>();
builder.Services.AddScoped<IDAOTipoUsuario, DAOTipoUsuario>();
builder.Services.AddScoped<IDAOUsuario, DAOUsuario>();
builder.Services.AddScoped<IDAOCategoriaPrato, DAOCategoriaPrato>();
builder.Services.AddScoped<IDAOPrato, DAOPrato>();
builder.Services.AddScoped<IDAOHistoricoDisponibilidade, DAOHistoricoDisponibilidade>();
builder.Services.AddScoped<IDAOPedido, DAOPedido>();
builder.Services.AddScoped<IDAOFormaPagamento, DAOFormaPagamento>();
builder.Services.AddScoped<IDAOPagamento, DAOPagamento>();
builder.Services.AddScoped<IDAODashboard, DAODashboard>();
builder.Services.AddScoped<IDAOAuditoria, DAOAuditoria>();

builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();
builder.Services.AddScoped<IFacadeMesa, FacadeMesa>();
builder.Services.AddScoped<IFacadeTipoUsuario, FacadeTipoUsuario>();
builder.Services.AddScoped<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddScoped<IFacadeCategoriaPrato, FacadeCategoriaPrato>();
builder.Services.AddScoped<IFacadePrato, FacadePrato>();
builder.Services.AddScoped<IFacadeCardapio, FacadeCardapio>();
builder.Services.AddScoped<IFacadePedido, FacadePedido>();
builder.Services.AddScoped<IFacadeFormaPagamento, FacadeFormaPagamento>();
builder.Services.AddScoped<IFacadePagamento, FacadePagamento>();
builder.Services.AddScoped<IFacadeDashboard, FacadeDashboard>();
builder.Services.AddScoped<IFacadeAuditoria, FacadeAuditoria>();


builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenJwtService>();

builder.Services.AddScoped<IImagemService, ImagemService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoNotificacaoService, PedidoNotificacaoService>();

builder.Services.AddScoped<IAuditoriaFactory, AuditoriaFactory>();
builder.Services.AddScoped<IUsuarioFactory, UsuarioFactory>();

builder.Services.AddSignalR();

builder.Services.Configure<MercadoPagoOptions>(builder.Configuration.GetSection("MercadoPago"));

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirMVC", policy => {
        policy
            .WithOrigins("https://localhost:7102")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents {
        OnMessageReceived = context => {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/pedidoHub")) {
                context.Token = accessToken;
            }

            return Task.CompletedTask; 
        }
    };
});


builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

    //Somente admins
    options.AddPolicy("SomenteAdmin", policy => policy.RequireRole("ADMINISTRADOR"));

    //Admin ou Gerente
    options.AddPolicy("AdminGerente", policy => policy.RequireRole("ADMINISTRADOR", "GERENTE"));

    //Atendimento (Garçom, Admin e Gerente)
    options.AddPolicy("Atendimento", policy => policy.RequireRole("ADMINISTRADOR", "GERENTE", "GARÇOM"));

    //Cozinha (Cozinha, Admin e Gerente)
    options.AddPolicy("Cozinha", policy => policy.RequireRole("ADMINISTRADOR", "GERENTE", "COZINHA"));

    //Caixa (Caixa, Admin e Gerente)
    options.AddPolicy("Caixa", policy => policy.RequireRole("ADMINISTRADOR", "GERENTE", "CAIXA"));

    //Criar Pedido (Garçom, Admin, Gerente, Chatbot)
    options.AddPolicy("AtendimentoComChatbot", policy => policy.RequireRole("ADMINISTRADOR", "GERENTE", "GARÇOM", "CHATBOT"));
});

builder.Services.AddHttpClient<IMercadoPagoService, MercadoPagoService>(client => {
    client.BaseAddress = new Uri("https://api.mercadopago.com/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("PermitirMVC");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.MapHub<PedidoHub>("/pedidoHub");

app.Run();

