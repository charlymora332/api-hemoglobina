namespace RetoHemoglobina.Models;

public class RespuestaGeneral
{
    public List<ResultadoPaciente> Pacientes { get; set; } = new();
    public ResumenTotales Totales { get; set; } = new();
}
