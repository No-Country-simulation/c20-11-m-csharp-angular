using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Tastys.API.Middlewares;
using Tastys.BLL.Services.Receta.RecetaCRUD;
using Tastys.BLL.Services.Review;
using Tastys.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBLLServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<UserServices>();

//Autenticacion
builder.Services.AddTransient<IAsyncAuthorizationFilter,CheckToken>();
builder.Services.AddTransient<IAsyncAuthorizationFilter,SetToken>();

builder.Services.AddScoped<RecetaCRUD>();

//Autenticacion
builder.Services.AddTransient<IAsyncAuthorizationFilter,CheckToken>();
builder.Services.AddTransient<IAsyncAuthorizationFilter,SetToken>();
builder.Services.AddScoped<ReviewCRUD>();


builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = string.Empty;  
    });


}
if (app.Environment.IsDevelopment())
{
    // Migra y agrega datos semilla a la DB si no existe.
    app.Services.InitialiseDatabase();
}


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
