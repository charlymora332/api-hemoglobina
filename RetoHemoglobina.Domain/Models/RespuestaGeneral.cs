// respuesta general que contiene la lista de resultados por paciente y el resumen de totales
// intancia lista de pacientes y resumen de totales
namespace RetoHemoglobina.Domain.Models;


public class RespuestaGeneral
{
    public List<ResultadoPaciente> Pacientes { get; set; } = new();
    public ResumenTotales Totales { get; set; } = new();
}
