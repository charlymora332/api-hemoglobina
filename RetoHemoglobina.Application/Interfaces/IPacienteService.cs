using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Intefaces;

public interface IPacienteService
{
    Task<RespuestaGeneralDTO?> ProcesarPacientesAsync(List<PacienteRequestDTO> pacienteRequestDTOs);

    Task<List<ResultadoPacienteDTO>> ListarConsultasAsync();

    Task<List<PacienteDTO>> ListarPacientesAsync();

    Task<List<ResultadoPacienteDTO>> ListarConsultasPorPacienteAsync(int identificacion);
}