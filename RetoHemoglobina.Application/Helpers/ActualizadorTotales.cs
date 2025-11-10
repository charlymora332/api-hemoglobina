using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Helpers
{
    public static class ActualizadorTotales
    {
        // Diccionario de combinaciones -> Acción
        private static readonly Dictionary<(GeneroEnum, TipoAlertaEnum), Action<ResumenTotales>> _acciones = new()
    {
        {(GeneroEnum.Hombre, TipoAlertaEnum.Bajo),   t => t.HombresAlerta1++},
        {(GeneroEnum.Hombre, TipoAlertaEnum.Alto),   t => t.HombresAlerta2++},
        {(GeneroEnum.Hombre, TipoAlertaEnum.Normal), t => t.HombresSin++},
        {(GeneroEnum.Mujer,  TipoAlertaEnum.Bajo),   t => t.MujeresAlerta1++},
        {(GeneroEnum.Mujer,  TipoAlertaEnum.Alto),   t => t.MujeresAlerta2++},
        {(GeneroEnum.Mujer,  TipoAlertaEnum.Normal), t => t.MujeresSin++}
    };

        public static void ActualizarTotales(ResumenTotales resumenTotales, GeneroEnum genero, TipoAlertaEnum tipoAlerta)
        {
            //// Crear una variable para guardar la acción encontrada (inicialmente nula)
            //Action<ResumenTotales>? accionEncontrada = null;

            //// Intentar obtener del diccionario la acción correspondiente al género y tipo de alerta
            //bool existeAccion = _acciones.TryGetValue((genero, tipoAlerta), out accionEncontrada);

            //// Si se encontró una acción, ejecutarla pasando el objeto resumenTotales
            //if (existeAccion && accionEncontrada != null)
            //{
            //    accionEncontrada(resumenTotales);
            //}

            if (_acciones.TryGetValue((genero, tipoAlerta), out Action<ResumenTotales> accion))
                accion(resumenTotales);
        }
    }
}