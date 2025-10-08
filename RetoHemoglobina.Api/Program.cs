using Microsoft.OpenApi.Models;
using RetoHemoglobina.IServices;
using RetoHemoglobina.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Agregar controladores
builder.Services.AddControllers();

// ✅ Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Reto Hemoglobina",
        Version = "v1"
    });
});

// ✅ Configurar CORS (libre en local)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ✅ Registrar el servicio IPacienteService con su implementación
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

// ✅ Swagger habilitado SIEMPRE
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Reto Hemoglobina v1");
    c.RoutePrefix = "swagger"; // URL = /swagger/index.html
});

// ✅ Usar CORS
app.UseCors("AllowAll");

// ❌ Desactivamos HTTPS en local (si quieres lo activas en deploy)
// app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ Mapear controladores
app.MapControllers();

// ✅ Arrancar en http://localhost:5000
app.Run("http://localhost:5000");

