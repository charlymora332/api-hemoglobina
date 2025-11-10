using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Intefaces;

public interface IPacienteService
{
    RespuestaGeneralDTO? ProcesarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs);

    Task<List<ResultadoPacienteDTO>> ListarConsultas();

    Task<List<PacienteDTO>> ListarPacientes();

    Task<List<ResultadoPacienteDTO>> ListarConsultasPorPaciente(int identificacion);

    //  void ListarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs);

    // Task<List<ResultadoPacienteDTO>> ListarConsultas();
}