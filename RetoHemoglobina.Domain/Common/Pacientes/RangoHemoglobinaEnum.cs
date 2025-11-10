namespace RetoHemoglobina.Domain.Common.Pacientes;

public static class RangoHemoglobinaEnum
{
    public static readonly Dictionary<GeneroEnum, (float Min, float Max)> Rangos = new()
    {
        { GeneroEnum.Mujer,  (13.2f, 16.6f) },
        { GeneroEnum.Hombre, (11.6f, 15.0f) }
    };
}