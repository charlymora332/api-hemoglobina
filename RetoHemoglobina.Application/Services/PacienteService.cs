using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Application.Helpers;
using RetoHemoglobina.Application.Intefaces;
using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Services;

public class PacienteService : IPacienteService
{
    private IPacienteRepository _pacienteRepository;

    public PacienteService(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public async Task<List<ResultadoPacienteDTO>> ListarConsultas()
    {
        try
        {
            var consultas = await _pacienteRepository.ListarConsultas();

            if (consultas == null || !consultas.Any())
                throw new InvalidOperationException("No se encontraron consultas disponibles.");

            return consultas;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocurrió un error en la capa de servicios al obtener las consultas.", ex);
        }
    }

    public async Task<List<PacienteDTO>> ListarPacientes()
    {
        try
        {
            var general = await Task.Run(() =>
            {

                var consultas = _pacienteRepository.ListarPacientes();
                var consultass = _pacienteRepository.ListarPacientes();
                var consultasss = _pacienteRepository.ListarPacientes();
                return (consultas, consultass, consultasss);
            });
 

            //if (consultas == null || !consultas.Any())
            //  throw new InvalidOperationException("No se encontraron pacientes disponibles.");
           //  Task.WaitAll(consultas,consultass, consultasss);

            return  general.consultas.Result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocurrió un error en la capa de servicios al obtener la lista pacientes.", ex);
        }
    }

    public async Task<List<ResultadoPacienteDTO>> ListarConsultasPorPaciente(int identificacion)
    {
        try
        {
            var consultas = await _pacienteRepository.ListarConsultasPorPaciente(identificacion);

            if (consultas == null || !consultas.Any())
                throw new InvalidOperationException("No se encontraron consultas disponibles.");

            return consultas;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocurrió un error en la capa de servicios al obtener consultas por paciente.", ex);
        }
    }

    public RespuestaGeneralDTO? ProcesarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs)
    {
        RespuestaGeneralDTO respuesta = new RespuestaGeneralDTO();
        try
        {
            var pacientesValidos = pacienteRequestDTOs.Where(p => p != null
                  && p.Identificacion != null
                  && !string.IsNullOrWhiteSpace(p.Nombre)
                  && p.GeneroId != null
                  && p.Nivel != null).Select(p => new PacienteRequestDTO
                  {
                      Identificacion = p.Identificacion,
                      Nombre = p.Nombre.Trim(),
                      GeneroId = p.GeneroId,
                      Nivel = p.Nivel
                  }).ToList();

            ProcesarPacientesValidos(pacientesValidos, respuesta);

            return respuesta;
        }
        catch (Exception ex)
        {
            // Error inesperado general
            throw new ApplicationException($"Error inesperado al procesar pacientes: {ex.Message}", ex);
        }
    }

    //Metodos privados
    //private Paciente? ValidarYMapear(PacienteRequestDTO dto, int id, RespuestaGeneralDTO respuesta)
    //{
    //    List<string> camposFaltantes = new List<string>();

    //    if (string.IsNullOrWhiteSpace(dto.Nombre)) camposFaltantes.Add("Nombre");
    //    if (dto.Genero == null) camposFaltantes.Add("Genero");
    //    if (dto.Nivel == null) camposFaltantes.Add("Nivel");

    //    if (camposFaltantes.Any())
    //    {
    //        respuesta.Excepcion.Add(new Excepcion
    //        {
    //            Id = id,
    //            Mensaje = $"Faltan campos obligatorios: {string.Join(", ", camposFaltantes)}"
    //        });
    //        return null;
    //    }

    //    return new Paciente
    //    {
    //        Nombre = dto.Nombre!,
    //        Genero = dto.Genero!.Value,
    //        Nivel = dto.Nivel!.Value
    //    };
    //}

    private void ProcesarPacientesValidos(List<PacienteRequestDTO> pacientes, RespuestaGeneralDTO respuesta)
    {
        for (int i = 0; i < pacientes.Count; i++)
        {
            var p = pacientes[i];
            try
            {
                ValidarNivelGeneroYRegistrar(p, i + 1, respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Excepcion.Add(new Excepcion
                {
                    Id = (i + 1),
                    Mensaje = ex.Message
                });
            }
        }
    }

    private void ValidarNivelGeneroYRegistrar(PacienteRequestDTO p, int id, RespuestaGeneralDTO respuesta)
    {
        if (p.Identificacion == 0)
            throw new ArgumentException($"Numero de identificacion: {p.Identificacion} no válido para paciente {id} -> {p.Nombre}");

        if (p.Nivel <= 0 || p.Nivel > 30)
            throw new ArgumentException($"Nivel {p.Nivel} no válido para paciente {id} -> {p.Nombre}, nivel permitido deber ser mayor a 0 y menor a 30 .");

        GeneroEnum genero = p.GeneroId switch
        {
            1 => GeneroEnum.Mujer,
            2 => GeneroEnum.Hombre,
            _ => throw new ArgumentException($"Genero {p.GeneroId} no válido para paciente {id} -> {p.Nombre}.")
        };

        (float min, float max) = RangoHemoglobinaEnum.Rangos[genero];

        TipoAlertaEnum tipoAlerta = p.Nivel switch
        {
            float nivel when nivel < min => TipoAlertaEnum.Bajo,
            float nivel when nivel > max => TipoAlertaEnum.Alto,
            _ => TipoAlertaEnum.Normal
        };

        string mensaje = MensajesAlertaEnum.Textos[tipoAlerta];

        ActualizadorTotales.ActualizarTotales(respuesta.Totales, genero, tipoAlerta);

        ResultadoPacienteDTO resultadoPaciente = new ResultadoPacienteDTO
        {
            Identificacion = p.Identificacion ?? 0,
            Nombre = p.Nombre,
            Genero = (byte)genero,
            Nivel = p.Nivel ?? 0,
            IdAlerta = (byte)tipoAlerta,
            Alerta = mensaje
        };

        respuesta.Pacientes.Add(resultadoPaciente);

        _pacienteRepository.RegistrarConsultaR(resultadoPaciente);
    }
}