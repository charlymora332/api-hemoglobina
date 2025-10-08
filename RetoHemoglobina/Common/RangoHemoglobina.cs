namespace RetoHemoglobina.Common;

public static class RangoHemoglobina
{
    public static readonly Dictionary<Genero, (float Min, float Max)> Rangos = new()
    {
        { Genero.Mujer,  (13.2f, 16.6f) },
        { Genero.Hombre, (11.6f, 15.0f) }
    };
}
