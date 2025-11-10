namespace RetoHemoglobina.Domain.Models;

//datos del que se reciben para procesar el paciente
public class Alerta
{
    public byte Id { get; set; } // PK

    public string TipoAlerta { get; set; }

    public ICollection<ResultadoPaciente> Resultados { get; set; }
}