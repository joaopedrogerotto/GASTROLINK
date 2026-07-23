using APIGastroLink.DAO;
using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Hubs;
using APIGastroLink.Services;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// Add services to the container.

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

builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();
builder.Services.AddScoped<IFacadeMesa, FacadeMesa>();
builder.Services.AddScoped<IFacadeTipoUsuario, FacadeTipoUsuario>();
builder.Services.AddScoped<IFacadeUsuario, FacadeUsuario>();
builder.Services.AddScoped<IFacadeCategoriaPrato, FacadeCategoriaPrato>();
builder.Services.AddScoped<IFacadePrato, FacadePrato>();
builder.Services.AddScoped<IFacadeCardapio, FacadeCardapio>();
builder.Services.AddScoped<IFacadePedido, FacadePedido>();

builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenJwtService>();

builder.Services.AddScoped<IImagemService, ImagemService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IPedidoNotificacaoService, PedidoNotificacaoService>();

builder.Services.AddSignalR();

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

