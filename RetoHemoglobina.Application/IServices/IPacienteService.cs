using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Application.IServices
{
 
        public interface IPacienteService
        {
            RespuestaGeneral ProcesarPacientes(List<Paciente> pacientes);
        }
    }




