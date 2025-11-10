namespace RetoHemoglobina.Domain.Common.Alertas;

public static class MensajesAlertaEnum
{
    public static readonly Dictionary<TipoAlertaEnum, string> Textos = new()
    {
        { TipoAlertaEnum.Normal, "Alerta 0 (Normal)" },
        { TipoAlertaEnum.Bajo,   "Alerta 1 (Bajo)" },
        { TipoAlertaEnum.Alto,   "Alerta 2 (Alto)" }
    };
}