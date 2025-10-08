using RetoHemoglobina.Models;

namespace RetoHemoglobina.IServices
{
 
        public interface IPacienteService
        {
            RespuestaGeneral ProcesarPacientes(List<Paciente> pacientes);
        }
    }


