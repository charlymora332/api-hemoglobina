using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;
using RetoHemoglobina.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoHemoglobina.Application.Helpers
{
    public static class ActualizadorTotales
    {
        // Diccionario de combinaciones -> Acción
        private static readonly Dictionary<(Genero, TipoAlerta), Action<ResumenTotales>> _acciones = new()
    {
        {(Genero.Hombre, TipoAlerta.Bajo),   t => t.HombresAlerta1++},
        {(Genero.Hombre, TipoAlerta.Alto),   t => t.HombresAlerta2++},
        {(Genero.Hombre, TipoAlerta.Normal), t => t.HombresSin++},
        {(Genero.Mujer,  TipoAlerta.Bajo),   t => t.MujeresAlerta1++},
        {(Genero.Mujer,  TipoAlerta.Alto),   t => t.MujeresAlerta2++},
        {(Genero.Mujer,  TipoAlerta.Normal), t => t.MujeresSin++}
    };

        public static void ActualizarTotales(ResumenTotales resumenTotales, Genero genero, TipoAlerta tipoAlerta)
        {

            if (_acciones.TryGetValue((genero, tipoAlerta), out Action<ResumenTotales> accion))
                accion(resumenTotales);
        }
    }

}
