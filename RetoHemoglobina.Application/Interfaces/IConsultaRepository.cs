using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.Intefaces
{
    public interface IConsultaRepository
    {
        //void RegistrarConsultaR(ResultadoPacienteDTO consulta);

        //  void ListarConsultas();
        Task<List<ResultadoPacienteDTO>> ListarConsultasAsync();

        Task<List<ResultadoPacienteDTO>> ListarConsultasPorPacienteAsync(int identificacion);

        Task RegistrarConsultaAsync(ResultadoPacienteDTO consulta);
    }
}