using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.IServices;


    public interface IPacienteService
    {
        RespuestaGeneralDTO ProcesarPacientes(List<PacienteRequestDTO> pacienteRequestDTOs);
    }





