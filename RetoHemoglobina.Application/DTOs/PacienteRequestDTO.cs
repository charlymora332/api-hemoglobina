namespace RetoHemoglobina.Application.DTOs
{
    public class PacienteRequestDTO
    {
        public int? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public byte? GeneroId { get; set; }// 1 = Femenino, 2 = Masculino
        public float? Nivel { get; set; }  // nivel deHemoglobina
    }
}