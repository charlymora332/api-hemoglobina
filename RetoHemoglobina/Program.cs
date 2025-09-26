using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// ── Forzar Kestrel a escuchar el puerto que provee Render (env var PORT)
//    Si no existe, usa 10000 como fallback (útil para pruebas locales)
builder.WebHost.ConfigureKestrel(options =>
{
    var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "10000";
    if (int.TryParse(portEnv, out var port))
    {
        options.ListenAnyIP(port);
    }
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Reto Hemoglobina",
        Version = "v1"
    });
});

// 👇 Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Habilitar Swagger en desarrollo o si defines la variable ENABLE_SWAGGER=true en Render
//var enableSwagger = app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("ENABLE_SWAGGER") == "true";
//if (enableSwagger)
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// ── Importante: procesar headers reenviados por el proxy (X-Forwarded-Proto)
//    Esto permite que UseHttpsRedirection() detecte correctamente HTTP/HTTPS
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

// 👇 Activar CORS ANTES de MapControllers
app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();








//using Microsoft.OpenApi.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "API Reto Hemoglobina",
//        Version = "v1"
//    });
//});

//// 👇 Agregar CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        policy => policy
//            .AllowAnyOrigin()   // Permitir cualquier origen (útil para pruebas)
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// 👇 Activar CORS ANTES de MapControllers
//app.UseCors("AllowReactApp");

//app.MapControllers();

//app.Run();
