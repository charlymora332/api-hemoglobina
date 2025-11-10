using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Intefaces
{
    public interface IPacienteRepository
    {
        //void RegistrarConsultaR(ResultadoPacienteDTO consulta);

        //  void ListarConsultas();
        //Task<List<ResultadoPacienteDTO>> ListarConsultas();

        Task<List<PacienteDTO>> ListarPacientesAsync();

        //Task<List<ResultadoPacienteDTO>> ListarConsultasPorPaciente(int identificacion);
        Task RegistrarPacienteAsync(Paciente paciente);

        Task<Paciente?> ObtenerPorIdentificacionAsync(int identificacion);
    }
}