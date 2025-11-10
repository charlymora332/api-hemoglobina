namespace RetoHemoglobina.Domain.Models;

//datos del que se reciben para procesar el paciente
public class Genero
{
    public byte Id { get; set; } // 1 = Masculino, 2 = Femenino
    public string TipoGenero { get; set; }

    public ICollection<Paciente> Pacientes { get; set; }
}