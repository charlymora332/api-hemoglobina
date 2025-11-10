using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Application.Helpers;
using RetoHemoglobina.Application.Intefaces;
using RetoHemoglobina.Domain.Common.Alertas;
using RetoHemoglobina.Domain.Common.Pacientes;
using RetoHemoglobina.Domain.Exceptions;
using RetoHemoglobina.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using RetoHemoglobina.Application.Validators;

namespace RetoHemoglobina.Services;

public class PacienteService : IPacienteService
{
    private IPacienteRepository _pacienteRepository;
    private IConsultaRepository _consultaRepository;

    public PacienteService(IPacienteRepository pacienteRepository, IConsultaRepository consultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _consultaRepository = consultaRepository;
    }

    public async Task<List<ResultadoPacienteDTO>> ListarConsultasAsync()
    {
        var consultas = await _consultaRepository.ListarConsultasAsync();

        if (consultas == null || !consultas.Any())
            throw new InvalidOperationException("No se encontraron consultas disponibles.");

        return consultas;
    }

    public async Task<List<PacienteDTO>> ListarPacientesAsync()
    {
        var consultas = await _pacienteRepository.ListarPacientesAsync();

        if (consultas == null || !consultas.Any())
            throw new InvalidOperationException("No se encontraron pacientes disponibles.");

        return consultas;
    }

    public async Task<List<ResultadoPacienteDTO>> ListarConsultasPorPacienteAsync(int identificacion)
    {
        var paciente = await _pacienteRepository.ObtenerPorIdentificacionAsync(identificacion);

        if (paciente == null)
            throw new InvalidOperationException($"No existe paciente con identificacion: {identificacion}");

        var consultas = await _consultaRepository.ListarConsultasPorPacienteAsync(identificacion);

        return consultas;
    }

    public async Task<RespuestaGeneralDTO?> ProcesarPacientesAsync(List<PacienteRequestDTO> pacienteRequestDTOs)
    {
        RespuestaGeneralDTO respuesta = new RespuestaGeneralDTO();
        try
        {
            var pacientesValidos = pacienteRequestDTOs
       .Where(p => p is { Identificacion: not null, Nombre: not null, GeneroId: not null, Nivel: not null })
       .Select(p => new PacienteRequestDTO
       {
           Identificacion = p.Identificacion!.Value,
           Nombre = p.Nombre.Trim(),
           GeneroId = p.GeneroId!.Value,
           Nivel = p.Nivel!.Value
       })
       .ToList();

            await ProcesarPacientesValidos(pacientesValidos, respuesta);

            return respuesta;
        }
        catch (Exception ex)
        {
            // Error inesperado general
            throw new ApplicationException($"Error inesperado al procesar pacientes: {ex.Message}", ex);
        }
    }

    private async Task ProcesarPacientesValidos(List<PacienteRequestDTO> pacientes, RespuestaGeneralDTO respuesta)
    {
        for (int i = 0; i < pacientes.Count; i++)
        {
            var p = pacientes[i];
            try
            {
                PacienteValidacion.Validar(p, i + 1);
                ResultadoPacienteDTO resultado = Calcular(p, respuesta);
                respuesta.Pacientes.Add(resultado);
                await VerificarYRegistrarPacienteAsync(resultado);
            }
            catch (ValidacionException vex)
            {
                foreach (var err in vex.Errores)
                    respuesta.Excepcion.Add(new Excepcion { Id = respuesta.Excepcion.Count + 1, Mensaje = err });
            }
        }
    }

    private ResultadoPacienteDTO Calcular(PacienteRequestDTO p, RespuestaGeneralDTO respuesta)
    {
        var genero = (GeneroEnum)p.GeneroId;

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

        return resultadoPaciente;
    }

    //private static void Validar(PacienteRequestDTO p, int id)
    //{
    //    var errores = new List<string>();

    //    if (p.Nivel <= 0 || p.Nivel > 30)
    //        errores.Add($"Nivel {p.Nivel} no válido para paciente {id} -> {p.Nombre}, debe ser >0 y <=30.");

    //    if (p.Identificacion == 0)
    //        errores.Add($"Número de identificación: {p.Identificacion} no válido para paciente {id} -> {p.Nombre}");

    //    GeneroEnum? generov = p.GeneroId switch
    //    {
    //        1 => GeneroEnum.Mujer,
    //        2 => GeneroEnum.Hombre,
    //        _ => null
    //    };

    //    if (!generov.HasValue)
    //        errores.Add($"Género {p.GeneroId} no válido para paciente {id} -> {p.Nombre}.");

    //    if (errores.Any())
    //        throw new ValidacionException(errores);
    //}

    private async Task VerificarYRegistrarPacienteAsync(ResultadoPacienteDTO datos)
    {
        var paciente = await _pacienteRepository.ObtenerPorIdentificacionAsync(datos.Identificacion);

        if (paciente == null)
        {
            var nuevoPaciente = new Paciente
            {
                Identificacion = datos.Identificacion,
                GeneroId = datos.Genero,
                Nombre = datos.Nombre
            };
            await _pacienteRepository.RegistrarPacienteAsync(nuevoPaciente);
        }

        await _consultaRepository.RegistrarConsultaAsync(datos);
    }
}

//private async Task ValidarNivelGeneroYRegistrar(PacienteRequestDTO p, int id, RespuestaGeneralDTO respuesta)
//{
//    Validar(p, id);

//    ResultadoPacienteDTO resultado = Calcular(p, respuesta);

//    respuesta.Pacientes.Add(resultado);
//    await VerificarYRegistrarPacienteAsync(resultado);

//}