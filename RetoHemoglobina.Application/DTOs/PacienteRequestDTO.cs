namespace RetoHemoglobina.Application.DTOs
{
    public class PacienteRequestDTO
    {
        public string? Nombre { get; set; } 
        public byte? Genero { get; set; }// 1 = Femenino, 2 = Masculino
        public float? Nivel { get; set; }  // nivel deHemoglobina
    }
}

