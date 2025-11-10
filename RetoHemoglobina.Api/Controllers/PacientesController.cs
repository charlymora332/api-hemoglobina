using Microsoft.AspNetCore.Mvc;
using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Application.Intefaces;

namespace RetoHemoglobina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        //private readonly IMapper _mapper;

        // Inyección de dependencia
        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        //Post para procesar paciente y guardar en db
        [HttpPost("procesar")]
        public IActionResult ProcesarPacientes([FromBody] List<PacienteRequestDTO> pacientesRequest)
        {
            if (pacientesRequest == null || pacientesRequest.Count == 0)
            {
                return BadRequest("Debe proporcionar una lista de pacientes.");
            }

            try
            {
                RespuestaGeneralDTO respuesta = _pacienteService.ProcesarPacientes(pacientesRequest);
                return Ok(respuesta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error en el procesamiento",
                    detalles = ex.Message
                });
            }
        }

        //Get para obteenr todos los pacientes
        [HttpGet("obtenerPacientes")]
        public async Task<IActionResult> ListarPacientes()
        {
            try
            {
                var respuesta = await _pacienteService.ListarPacientes();
                return Ok(respuesta);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al intentar obtener la lista pacientes",
                    detalles = ex.Message
                });
            }
        }

        //Get para obtener  consultas por paciente
        [HttpGet("obtenerPorId/{identificacion}")]
        public async Task<IActionResult> ListarConsultasPorPaciente(int identificacion)
        {
            try
            {
                var respuesta = await _pacienteService.ListarConsultasPorPaciente(identificacion);
                return Ok(respuesta);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al intentar obtener las consultas por paciente",
                    detalles = ex.Message
                });
            }
        }

        //Get para obtener todas las consultas
        [HttpGet("obtener")]
        public async Task<IActionResult> ListarConsultas()
        {
            try
            {
                var respuesta = await _pacienteService.ListarConsultas();
                return Ok(respuesta);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al intentar obtener los pacientes",
                    detalles = ex.Message
                });
            }
        }
    }
}