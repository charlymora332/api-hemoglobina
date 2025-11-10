using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Intefaces
{
    public interface IPacienteRepository
    {
        void RegistrarConsultaR(ResultadoPacienteDTO consulta);

        //  void ListarConsultas();
        Task<List<ResultadoPacienteDTO>> ListarConsultas();

        Task<List<PacienteDTO>> ListarPacientes();

        Task<List<ResultadoPacienteDTO>> ListarConsultasPorPaciente(int identificacion);
    }
}