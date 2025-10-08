// modelo para devolver el resultado del análisis de hemoglobina por paciente 
namespace RetoHemoglobina.Domain.Models;

public class ResultadoPaciente
{
    public string Nombre { get; set; } 
    public byte Genero { get; set; }
    public float Nivel { get; set; }

    public byte IdAlerta { get; set; }        // 0 = Sin alerta, 1 = Bajo, 2 = Alto
    public string Alerta { get; set; } 
}