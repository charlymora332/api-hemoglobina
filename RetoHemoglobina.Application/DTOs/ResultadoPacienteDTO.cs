namespace RetoHemoglobina.Application.DTOs
{
    public class ResultadoPacienteDTO
    {
        public int Identificacion { get; set; }
        public string Nombre { get; set; }
        public byte Genero { get; set; }

        public float Nivel { get; set; }
        public byte IdAlerta { get; set; }
        public string Alerta { get; set; }
    }
}