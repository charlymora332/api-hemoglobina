namespace RetoHemoglobina.Models
{
    public class Paciente
    {
        public string Nombre { get; set; } = string.Empty;
        public int Genero { get; set; }      // 1 = Femenino, 2 = Masculino
        public double Nivel { get; set; }    // Hemoglobina
    }
}
