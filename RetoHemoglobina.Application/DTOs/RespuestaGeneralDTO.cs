using RetoHemoglobina.Domain.Models;
namespace RetoHemoglobina.Application.DTOs;

// respuesta general que contiene la lista de resultados por paciente , el resumen de totales y lista de excepciones
public class RespuestaGeneralDTO
{
    public List<ResultadoPaciente> Pacientes { get; set; } = new();
    public ResumenTotales Totales { get; set; } = new();

    public List<Excepcion>? Excepcion { get; set; } = new();
}
