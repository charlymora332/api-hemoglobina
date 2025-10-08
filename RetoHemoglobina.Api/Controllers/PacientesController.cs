using Microsoft.AspNetCore.Mvc;

using RetoHemoglobina.Application;
using RetoHemoglobina.Application;
using AutoMapper;
using RetoHemoglobina.Domain.Models;
using RetoHemoglobina.Application.IServices;
using RetoHemoglobina.Application.DTOs;

namespace RetoHemoglobina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;


        // Inyección de dependencia
        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }



        // Inyección de dependencias
        //public PacientesController(IPacienteService pacienteService, IMapper mapper)
        //{
        //    _pacienteService = pacienteService;
        //    _mapper = mapper;
        //}

        // POST: api/pacientes/procesar
        [HttpPost("procesar")]
        public IActionResult ProcesarPacientes([FromBody] List<PacienteRequest> pacientesRequest)
        {
            if (pacientesRequest == null || pacientesRequest.Count == 0)
            {
                return BadRequest("Debe proporcionar una lista de pacientes.");
            }

            try
            {
                // Llamar al servicio para procesar los pacientes
                var pacientes = pacientesRequest.Select(p => new Paciente
                {
                    Nombre = p.Nombre,
                    Genero = p.Genero,
                    Nivel = p.Nivel
                }).ToList();




                var respuesta = _pacienteService.ProcesarPacientes(pacientes);



                // ✅ Mapeamos DTO → Modelo de dominio
                //var pacientes = _mapper.Map<List<Paciente>>(pacientesRequest)

                //// ✅ Llamamos al servicio
                //var respuesta = _pacienteService.ProcesarPacientes(pacientes);

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

