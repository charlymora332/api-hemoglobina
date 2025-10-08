//datos del que se reciben para procesar el paciente
namespace RetoHemoglobina.Domain.Models

{
    public class Paciente
    {
        public string Nombre { get; set; }
        public byte Genero { get; set; }      // 1 = Femenino, 2 = Masculino
        public float Nivel { get; set; }    // nivel deHemoglobina
    }
}
