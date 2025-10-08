using Microsoft.AspNetCore.Mvc;
using RetoHemoglobina.Models;
using RetoHemoglobina.IServices;
using RetoHemoglobina.Application.IServices;

namespace RetoHemoglobina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        // Inyección de dependencia
        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // POST: api/pacientes/procesar
        [HttpPost("procesar")]
        public IActionResult ProcesarPacientes([FromBody] List<Paciente> pacientes)
        {
            if (pacientes == null || pacientes.Count == 0)
            {
                return BadRequest("Debe proporcionar una lista de pacientes.");
            }

            try
            {
                // Llamar al servicio para procesar los pacientes
                var respuesta = _pacienteService.ProcesarPacientes(pacientes);

                // Retornar los resultados procesados
                return Ok(respuesta);
            }
            catch (ArgumentException ex)
            {
                // Manejo de errores de validación
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Error genérico
                return StatusCode(500, new { mensaje = "Ocurrió un error en el procesamiento", detalles = ex.Message });
            }
        }
    }
}

