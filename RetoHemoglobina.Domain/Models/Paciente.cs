namespace RetoHemoglobina.Domain.Models;

//datos del que se reciben para procesar el paciente
public class Paciente
{
    public int Identificacion { get; set; } // PK

    public string Nombre { get; set; }

    // FK tipo byte
    public byte GeneroId { get; set; }

    public Genero Genero { get; set; }
    public ICollection<ResultadoPaciente> Resultados { get; set; }
}