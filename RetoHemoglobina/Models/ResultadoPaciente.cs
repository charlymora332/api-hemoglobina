namespace RetoHemoglobina.Models;

public class ResultadoPaciente
{
    public string Nombre { get; set; } = string.Empty;
    public int Genero { get; set; }
    public double Nivel { get; set; }

    public int IdAlerta { get; set; }        // 0 = Sin alerta, 1 = Bajo, 2 = Alto
    public string Alerta { get; set; } = ""; // Texto descriptivo
}