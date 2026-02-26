using APInegocy.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Negocy API",
        Version = "v1"
    });
});


// 🔥 1️⃣ REGISTRAR DB CONTEXT
builder.Services.AddDbContext<NegocyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// 🔥 2️⃣ REGISTRAR REPOSITORY
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 🔥 3️⃣ REGISTRAR SERVICE LAYER
builder.Services.AddScoped(typeof(IServiceManager<>), typeof(ServiceManager<>));


var app = builder.Build();


// 🔹 Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Negocy API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();