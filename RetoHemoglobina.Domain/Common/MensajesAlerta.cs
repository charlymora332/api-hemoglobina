namespace RetoHemoglobina.Domain.Common;

public static class MensajesAlerta
{
    public static readonly Dictionary<TipoAlerta, string> Textos = new()
    {
        { TipoAlerta.Normal, "Alerta 0 (Normal)" },
        { TipoAlerta.Bajo,   "Alerta 1 (Bajo)" },
        { TipoAlerta.Alto,   "Alerta 2 (Alto)" }
    };
}
