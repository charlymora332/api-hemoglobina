using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoHemoglobina.Domain.Common.Alertas;


public static class MensajesAlerta
{
    public static readonly Dictionary<TipoAlerta, string> Textos = new()
    {
        { TipoAlerta.Normal, "Alerta 0 (Normal)" },
        { TipoAlerta.Bajo,   "Alerta 1 (Bajo)" },
        { TipoAlerta.Alto,   "Alerta 2 (Alto)" }
    };
}

