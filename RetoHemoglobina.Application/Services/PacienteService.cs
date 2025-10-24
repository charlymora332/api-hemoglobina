
using RetoHemoglobina.Application.Helpers;
using RetoHemoglobina.Application.IServices;
using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;
using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Services;
public class PacienteService : IPacienteService
{
    public RespuestaGeneralDTO ProcesarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs)
    {
        RespuestaGeneralDTO respuesta = new RespuestaGeneralDTO();

        try
        {
            List<Paciente> pacientesValidos = new List<Paciente>();

            for (int i = 0; i < pacienteRequestDTOs.Count; i++)
            {
                PacienteRequestDTO dto = pacienteRequestDTOs[i];
                int id = i + 1;

                // Validar y mapear
                Paciente paciente = ValidarYMapear(dto, id, respuesta);

                if (paciente != null)
                {
                    pacientesValidos.Add(paciente);
                }
            }

            ProcesarPacientesValidos(pacientesValidos, respuesta);
        }
        catch (Exception ex)
        {
            // Error inesperado general
            throw new ApplicationException($"Error inesperado al procesar pacientes: {ex.Message}", ex);
        }

        return respuesta;
    }
 

    // ---------------------
    // Métodos privados
    // ---------------------
    private Paciente? ValidarYMapear(PacienteRequestDTO dto, int id, RespuestaGeneralDTO respuesta)
    {
        List<string> camposFaltantes = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Nombre)) camposFaltantes.Add("Nombre");
        if (dto.Genero == null) camposFaltantes.Add("Genero");
        if (dto.Nivel == null) camposFaltantes.Add("Nivel");

        if (camposFaltantes.Any())
        {
            respuesta.Excepcion.Add(new Excepcion
            {
                Id = (byte)id,
                Mensaje = $"Faltan campos obligatorios: {string.Join(", ", camposFaltantes)}"
            });
            return null;
        }

        return new Paciente
        {
            Nombre = dto.Nombre!,
            Genero = dto.Genero!.Value,
            Nivel = dto.Nivel!.Value
        };
    }

    private void ProcesarPacientesValidos(List<Paciente> pacientes, RespuestaGeneralDTO respuesta)
    {
        for (int i = 0; i < pacientes.Count; i++)
        {
            Paciente p = pacientes[i];
            try
            {
                ValidarNivelGeneroYRegistrar(p, i + 1, respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Excepcion.Add(new Excepcion
                {
                    Id = (byte)(i + 1),
                    Mensaje = ex.Message
                });
            }
        }
    }

    private void ValidarNivelGeneroYRegistrar(Paciente p, int id, RespuestaGeneralDTO respuesta)
    {
        if (p.Nivel <= 0 || p.Nivel > 30)
            throw new ArgumentException($"Nivel {p.Nivel} no válido para paciente {id} -> {p.Nombre}.");

        Genero genero = p.Genero switch
        {
            1 => Genero.Mujer,
            2 => Genero.Hombre,
            _ => throw new ArgumentException($"Genero {p.Genero} no válido para paciente {id} -> {p.Nombre}.")
        };

        (float min, float max) = RangoHemoglobina.Rangos[genero];

        TipoAlerta tipoAlerta = p.Nivel switch
        {
            float nivel when nivel < min => TipoAlerta.Bajo,
            float nivel when nivel > max => TipoAlerta.Alto,
            _ => TipoAlerta.Normal
        };

        string mensaje = MensajesAlerta.Textos[tipoAlerta];

        ActualizadorTotales.ActualizarTotales(respuesta.Totales, genero, tipoAlerta);

        respuesta.Pacientes.Add(new ResultadoPaciente
        {
            Nombre = p.Nombre,
            Genero = genero,
            Nivel = p.Nivel,
            IdAlerta = tipoAlerta,
            Alerta = mensaje
        });
    }
}




//using RetoHemoglobina.Application.Helpers;
//using RetoHemoglobina.Application.IServices;
//using RetoHemoglobina.Domain.Common.Alertas;
//using RetoHemoglobina.Domain.Common.Pacientes;
//using RetoHemoglobina.Application.DTOs;
//using RetoHemoglobina.Domain.Models;

//namespace RetoHemoglobina.Services
//{
//    public class PacienteService : IPacienteService
//    {
//        public RespuestaGeneralDTO ProcesarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs)


//        {


//            try
//            {
//                var respuesta = new RespuestaGeneralDTO();


//                var pacientesValidos = new List<Paciente>();

//                 int indexIdRespuesta = 0;



//                for (int i = 0; i < pacienteRequestDTOs.Count; i++)
//                {
//                    var dto = pacienteRequestDTOs[i];

//                    // Validar campos obligatorios
//                    if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Genero == null || dto.Nivel == null)
//                    {
//                        respuesta.Excepcion.Add(new Excepcion
//                        {
//                            Id = (i + 1),
//                            Mensaje = $"Faltan campos obligatorios en el paciente {i + 1}."
//                        });
//                        continue; // Saltar este paciente
//                    }

//                    // Mapear al dominio de manera segura
//                    pacientesValidos.Add(new Paciente
//                    {
//                        Nombre = dto.Nombre,
//                        Genero = dto.Genero.Value,
//                        Nivel = dto.Nivel.Value
//                    });
//                }



//                if (pacientesValidos == null || pacientesValidos.Count == 0)
//                {
//                    throw new ArgumentException("No se recibieron pacientes validos.");
//                }






//                for (int i = 0; i < pacientesValidos.Count; i++)
//                {
//                    var p = pacientesValidos[i];
//                    try
//                    {

//                        //if (p.Nombre is null || p.Genero is null || p.Nivel is null)
//                        //    throw new ArgumentException("Faltan campos obligatorios en el paciente.");



//                        // Validaciones de nivel de hemoglobina sea válido
//                        if (p.Nivel <= 0 || p.Nivel > 30)
//                        {
//                            throw new ArgumentException($"El nivel {p.Nivel} no es válido (0 < nivel ≤ 30) para el paciente {i + 1} -> {p.Nombre}. ");
//                        }

//                        // Obtener género
//                        var genero = p.Genero switch
//                        {
//                            1 => Genero.Mujer,
//                            2 => Genero.Hombre,
//                            _ => throw new ArgumentException($"El género {p.Genero} no es válido  para el paciente {i + 1} -> {p.Nombre}.")
//                        };

//                        // Obtener rango    
//                        var (min, max) = RangoHemoglobina.Rangos[genero];

//                        //Asignacion de tipo de alerta Id
//                        TipoAlerta tipoAlerta = p.Nivel switch
//                        {
//                            var nivel when nivel < min => TipoAlerta.Bajo,
//                            var nivel when nivel > max => TipoAlerta.Alto,
//                            _ => TipoAlerta.Normal
//                        };

//                        //Asignacion de mensaje de alerta por medio del id desde el diccionario
//                        var mensaje = MensajesAlerta.Textos[tipoAlerta];

//                        // Agregar resultado de paciente a Resumenes totales 
//                        ActualizadorTotales.ActualizarTotales(respuesta.Totales, genero, tipoAlerta);

//                        // Agregar resultado del paciente a la respuesta
//                        respuesta.Pacientes.Add(new ResultadoPaciente
//                        {
//                            Nombre = p.Nombre,
//                            Genero = (byte)genero,
//                            Nivel = p.Nivel,
//                            IdAlerta = (byte)tipoAlerta,
//                            Alerta = mensaje
//                        });
//                    }

//                    //Retornar excepciones controladas por paciente
//                    catch (ArgumentException ex)
//                    {
//                        respuesta.Excepcion.Add(new Excepcion
//                        {
//                            Id = (byte)(i + 1),
//                            Mensaje = ex.Message
//                        });
//                    }

//                    //Retornar excepciones no controladas por paciente
//                    catch (Exception ex)
//                    {
//                        respuesta.Excepcion.Add(new Excepcion
//                        {
//                            Id = (byte)(i + 1),
//                            Mensaje = ex.Message
//                        });
//                    }
//                }
//                return respuesta;
//            }

//            //Retornar excepciones si no se reciben pacientes
//            catch (ArgumentException ex)
//            {
//                throw new ApplicationException($"Error en los datos: {ex.Message}");
//            }

//            //Retornar excepciones no controladas generales
//            catch (Exception ex)
//            {
//                throw new ApplicationException($"Error inesperado al procesar pacientes: {ex.Message}");
//            }
//        }
//    }
//}

