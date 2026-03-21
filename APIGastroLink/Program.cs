using APIGastroLink.DAO;
using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade;
using APIGastroLink.Facade.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDAODatabase, DAOSQLServer>();
builder.Services.AddScoped<IDAOLogin, DAOLogin>();
builder.Services.AddScoped<IDAOMesa, DAOMesa>();

builder.Services.AddScoped<IFacadeLogin, FacadeLogin>();
builder.Services.AddScoped<IFacadeMesa, FacadeMesa>();

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirMVC", policy => {
        policy
            .WithOrigins("https://localhost:7102")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("PermitirMVC");

app.UseAuthorization();

app.MapControllers();

app.Run();

