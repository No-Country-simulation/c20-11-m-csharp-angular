using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tastys.API.Middlewares;
using Tastys.BLL.Services;
using Tastys.BLL.Services.RecetaCRUD;
using Tastys.BLL.Services.Review;
using Tastys.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBLLServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<UserServices>();

builder.Services.AddScoped<RecetaCRUD>();

//Autenticacion
builder.Services.AddTransient<IAsyncAuthorizationFilter,CheckToken>();
builder.Services.AddTransient<IAsyncAuthorizationFilter,SetToken>();
builder.Services.AddTransient<IAsyncAuthorizationFilter,CheckPermissions>();
builder.Services.AddScoped<ReviewCRUD>();

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        // Convertir enums de los DTOs a strings para que sea mas legible para el front
        var enumConverter = new JsonStringEnumConverter(JsonNamingPolicy.CamelCase);
        opts.JsonSerializerOptions.Converters.Add(enumConverter);

        opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Configurar el middleware de manejo de errores
builder.Services
    .AddExceptionHandler<ErrorHandler>()
    .AddProblemDetails()
    .Configure<ApiBehaviorOptions>(opts =>
    {
        opts.SuppressModelStateInvalidFilter = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
    c.ExampleFilters();

    // Incluir comentarios en Swagger
    foreach (var filePath in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!), "*.xml"))
    {
        try
        {
            c.IncludeXmlComments(filePath, includeControllerXmlComments: true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

//CORS para el Cliente
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
// No exponer Swagger en producción: https://medium.com/@tommy.adeoye/exploring-the-risks-of-leaving-swagger-pages-on-production-apis-sensitive-data-exposure-and-a20c7345c468
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
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

app.UseCors("AllowSpecificOrigin");
app.UseCookiePolicy();

app.UseExceptionHandler();

app.UseAuthorization();
app.MapControllers();

app.Run();
