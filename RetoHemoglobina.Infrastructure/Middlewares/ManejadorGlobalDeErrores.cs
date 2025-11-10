using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;

public class ManejadorGlobalDeErrores
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ManejadorGlobalDeErrores> _logger;

    public ManejadorGlobalDeErrores(RequestDelegate next, ILogger<ManejadorGlobalDeErrores> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error no manejado");

            context.Response.ContentType = "application/json";

            // Elegir código HTTP según tipo de excepción
            context.Response.StatusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                mensaje = ex.Message,
                tipo = ex.GetType().Name
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}