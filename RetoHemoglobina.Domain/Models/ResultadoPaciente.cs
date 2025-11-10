namespace RetoHemoglobina.Domain.Models;
// modelo para devolver el resultado del análisis de hemoglobina por paciente

public class ResultadoPaciente
{
    // Clave primaria
    public int Id { get; set; }

    public float Nivel { get; set; }

    // Relación con tabla de alertas
    public byte AlertaId { get; set; }

    public Alerta Alerta { get; set; }

    // Clave foránea con paciente
    public int Identificacion { get; set; }

    public Paciente Paciente { get; set; }
}