//using Micrusing Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ✅ importa EF Core
using Microsoft.OpenApi.Models;
using RetoHemoglobina.Application.Intefaces;
using RetoHemoglobina.Infrastructure; // ✅ importa tu contexto
using RetoHemoglobina.Infrastructure.Repositories;
using RetoHemoglobina.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ Agregar configuración del DbContext con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ 2️⃣ Agregar controladores
builder.Services.AddControllers();

// ✅ 3️⃣ Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Reto Hemoglobina",
        Version = "v1"
    });
});

// ✅ 4️⃣ Configurar CORS (libre)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ✅ 5️⃣ Registrar servicios personalizados
//builder.Services.AddScoped<IPacienteService, PacienteService>();
// Program.cs
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

// ✅ 6️⃣ Configurar respuesta de error por validaciones
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new
            {
                Campo = e.Key,
                Mensaje = e.Value?.Errors.First().ErrorMessage
            })
            .ToList();

        var respuesta = new
        {
            Exito = false,
            Mensaje = "La solicitud contiene errores de formato o escritura.",
            Errores = errors
        };

        return new BadRequestObjectResult(respuesta);
    };
});

var app = builder.Build();

// ✅ 7️⃣ Swagger siempre activo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Reto Hemoglobina v1");
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger"; // URL = /swagger/index.html
    });
});

// ✅ 8️⃣ Usar CORS
app.UseCors("AllowAll");

// ✅ 9️⃣ HTTPS opcional
// app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ 🔟 Mapear controladores
app.MapControllers();

// ✅ 🔟 Escuchar en el puerto que Render o local asigna
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();

//osoft.AspNetCore.Mvc;
//using Microsoft.OpenApi.Models;
//using RetoHemoglobina.Application.Intefaces;
//using RetoHemoglobina.Services;
//using Microsoft.EntityFrameworkCore; // ✅ importa EF Core
//using RetoHemoglobina.Infrastructure;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Agregar controladores
//builder.Services.AddControllers();

//// ✅ Configurar Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "API Reto Hemoglobina",
//        Version = "v1"
//    });
//});

//// ✅ Configurar CORS (libre)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader());
//});

//// ✅ Registrar servicios
//builder.Services.AddScoped<IPacienteService, PacienteService>();
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.InvalidModelStateResponseFactory = context =>
//    {
//        var errors = context.ModelState
//            .Where(e => e.Value?.Errors.Count > 0)
//            .Select(e => new
//            {
//                Campo = e.Key,
//                Mensaje = e.Value?.Errors.First().ErrorMessage
//            })
//            .ToList();

//        var respuesta = new
//        {
//            Exito = false,
//            Mensaje = "La solicitud contiene errores de formato o escritura.",
//            Errores = errors
//        };

//        return new BadRequestObjectResult(respuesta);
//    };
//});

//var app = builder.Build();

//// ✅ Swagger siempre activo
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Reto Hemoglobina v1");
//    c.RoutePrefix = "swagger"; // URL = /swagger/index.html
//});

//// ✅ Usar CORS
//app.UseCors("AllowAll");

//// ❌ HTTPS opcional
//// app.UseHttpsRedirection();

//app.UseAuthorization();

//// ✅ Mapear controladores
//app.MapControllers();

//// ✅ Escuchar en el puerto que Render asigna
//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//app.Urls.Add($"http://*:{port}");

//app.Run();