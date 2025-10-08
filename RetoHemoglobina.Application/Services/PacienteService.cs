using RetoHemoglobina.Application.IServices;
using RetoHemoglobina.Domain.Common;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Services
{
    public class PacienteService : IPacienteService
    {
        public RespuestaGeneral ProcesarPacientes(List<Paciente> pacientes)

        //validacion si se hace la peticon y no hay pacientes
        {
            if (pacientes == null || pacientes.Count == 0)
                throw new ArgumentException("No se recibieron pacientes.");

            //definir la respuesta general que es la que se va a retornar

            var respuesta = new RespuestaGeneral();

            foreach (var p in pacientes)
            {
                // Validación de nivel usando switch + pattern (sin if)
                switch (p.Nivel)
                {
                    case var nivel when nivel <= 0 || nivel > 30:
                        throw new ArgumentException($"El nivel {p.Nivel} no es válido (0 < nivel ≤ 30)");
                }

                // Mapear género con switch expression (1 => Mujer, 2 => Hombre, otro => error)
                var genero = p.Genero switch
                {
                    1 => Genero.Mujer,
                    2 => Genero.Hombre,
                    _ => throw new ArgumentException($"El género {p.Genero} no es válido.")
                };

                // Obtener rango
                var (min, max) = RangoHemoglobina.Rangos[genero];

                // Determinar tipo de alerta con switch expression (sin if)
                TipoAlerta tipoAlerta = p.Nivel switch
                {
                    var nivel when nivel < min => TipoAlerta.Bajo,
                    var nivel when nivel > max => TipoAlerta.Alto,
                    _ => TipoAlerta.Normal
                };

                // Mensaje desde el diccionario
                var mensaje = MensajesAlerta.Textos[tipoAlerta];

                // Agregar resultado individual
                respuesta.Pacientes.Add(new ResultadoPaciente
                {
                    Nombre = p.Nombre,
                    Nivel = p.Nivel,
                    IdAlerta = (byte)tipoAlerta,
                    Alerta = mensaje
                });

                // Actualizar totales usando switches (sin if)
                switch (genero)
                {
                    case Genero.Mujer:
                        switch (tipoAlerta)
                        {

                            case TipoAlerta.Bajo:
                                respuesta.Totales.MujeresAlerta1++;
                                break;
                            case TipoAlerta.Alto:
                                respuesta.Totales.MujeresAlerta2++;
                                break;
                            case TipoAlerta.Normal:
                                respuesta.Totales.MujeresSin++;
                                break;
                        }
                        break;

                    case Genero.Hombre:
                        switch (tipoAlerta)
                        {
                            case TipoAlerta.Bajo:
                                respuesta.Totales.HombresAlerta1++;
                                break;
                            case TipoAlerta.Alto:
                                respuesta.Totales.HombresAlerta2++;
                                break;
                            case TipoAlerta.Normal:
                                respuesta.Totales.HombresSin++;
                                break;
                        }
                        break;
                }
            }

            return respuesta;
        }
    }
}

