
using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;

namespace RetoHemoglobina.Domain.Models;

// modelo que guarda los totales de alertas por género
public class ResumenTotales
{
    public int MujeresAlerta1 { get; set; }
    public int MujeresAlerta2 { get; set; }
    public int MujeresSin { get; set; }
    public int HombresAlerta1 { get; set; }
    public int HombresAlerta2 { get; set; }
    public int HombresSin { get; set; }
}


