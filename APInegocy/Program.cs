using APInegocy.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services to the container
builder.Services.AddControllers();

// 🔹 CORS: permitir todo (opcional, seguro para pruebas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Negocy API",
        Version = "v1"
    });
});

// 🔥 DB CONTEXT
builder.Services.AddDbContext<NegocyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 🔥 REPOSITORY
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 🔥 SERVICE LAYER
builder.Services.AddScoped(typeof(IServiceManager<>), typeof(ServiceManager<>));

var app = builder.Build();

// 🔹 Swagger en desarrollo y producción (opcional)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Negocy API v1");
});

// 🔹 CORS
app.UseCors("AllowAll");

// 🔹 Render NO necesita HTTPS obligatorio
// app.UseHttpsRedirection(); // comentar para Render

app.UseAuthorization();

app.MapControllers();

// 🔹 Escuchar puerto asignado por Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();