using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;

namespace RetoHemoglobina.Domain.Models;
// modelo para devolver el resultado del análisis de hemoglobina por paciente 

public class ResultadoPaciente
{
    public required string Nombre { get; set; } 
    public Genero Genero { get; set; }
    public float Nivel { get; set; }

    public TipoAlerta IdAlerta { get; set; }        // 0 = Sin alerta, 1 = Bajo, 2 = Alto
    public required string Alerta { get; set; } 
}