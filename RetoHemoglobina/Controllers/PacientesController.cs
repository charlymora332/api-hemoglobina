using Microsoft.AspNetCore.Mvc;
using RetoHemoglobina.Models;

namespace RetoHemoglobina.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase


{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { mensaje = "API Hemoglobina funcionando" });
    }



    [HttpPost("procesar")]
    public ActionResult<RespuestaGeneral> ProcesarPacientes([FromBody] List<Paciente> pacientes)
    {
        var respuesta = new RespuestaGeneral();

        foreach (var p in pacientes)
        {
            int idAlerta;
            string alerta;

            // Evaluar mujeres (1 = Femenino)
            if (p.Genero == 1)
            {
                if (p.Nivel < 13.2)
                {
                    idAlerta = 1;
                    alerta = "Alerta 1 (Bajo)";
                }
                else if (p.Nivel > 16.6)
                {
                    idAlerta = 2;
                    alerta = "Alerta 2 (Alto)";
                }
                else
                {
                    idAlerta = 0;
                    alerta = "Alerta 0 (Normal)";
                }
            }
            // Evaluar hombres (2 = Masculino)
            else if (p.Genero == 2)
            {
                if (p.Nivel < 11.6)
                {
                    idAlerta = 1;
                    alerta = "Alerta 1 (Bajo)";
                }
                else if (p.Nivel > 15)
                {
                    idAlerta = 2;
                    alerta = "Alerta 2 (Alto)";
                }
                else
                {
                    idAlerta = 0;
                    alerta = "Alerta 0 (Normal)";
                }
            }
            // Si el género no es válido
            else
            {
                return BadRequest($"El género {p.Genero} no es válido. Use 1 (Femenino) o 2 (Masculino).");
            }

            // Guardar resultado individual del paciente
            respuesta.Pacientes.Add(new ResultadoPaciente
            {
                Nombre = p.Nombre,
                Genero = p.Genero,
                Nivel = p.Nivel,
                IdAlerta = idAlerta,
                Alerta = alerta
            });

            // Actualizar totales por género y alerta
            if (p.Genero == 1) // Mujer
            {
                if (idAlerta == 1) respuesta.Totales.MujeresAlerta1++;
                else if (idAlerta == 2) respuesta.Totales.MujeresAlerta2++;
                else respuesta.Totales.MujeresSin++;
            }
            else if (p.Genero == 2) // Hombre
            {
                if (idAlerta == 1) respuesta.Totales.HombresAlerta1++;
                else if (idAlerta == 2) respuesta.Totales.HombresAlerta2++;
                else respuesta.Totales.HombresSin++;
            }
        }

        // Devolver todo el resultado como JSON
        return Ok(respuesta);
    }
}









//using Microsoft.AspNetCore.Mvc;
//using RetoHemoglobina.Models;

//namespace RetoHemoglobina.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class PacientesController : ControllerBase
//{
//    [HttpPost("procesar")]
//    public ActionResult<RespuestaGeneral> ProcesarPacientes([FromBody] List<Paciente> pacientes)
//    {
//        var respuesta = new RespuestaGeneral();

//        foreach (var p in pacientes)
//        {
//            int idAlerta = 0;
//            string alerta = "Sin alerta";

//            if (p.Genero == 1) // Mujer
//            {
//                if (p.Nivel < 13.2) { idAlerta = 1; alerta = "Alerta 1 (Bajo)"; }
//                else if (p.Nivel > 16.6) { idAlerta = 2; alerta = "Alerta 2 (Alto)"; }
//            }
//            else if (p.Genero == 2) // Hombre
//            {
//                if (p.Nivel < 11.6) { idAlerta = 1; alerta = "Alerta 1 (Bajo)"; }
//                else if (p.Nivel > 15) { idAlerta = 2; alerta = "Alerta 2 (Alto)"; }
//            }

//            // Agregar paciente con resultado
//            respuesta.Pacientes.Add(new ResultadoPaciente
//            {
//                Nombre = p.Nombre,
//                Genero = p.Genero,
//                Nivel = p.Nivel,
//                IdAlerta = idAlerta,
//                Alerta = alerta
//            });

//            // Contadores
//            if (p.Genero == 1)
//            {
//                if (idAlerta == 1) respuesta.Totales.MujeresAlerta1++;
//                else if (idAlerta == 2) respuesta.Totales.MujeresAlerta2++;
//                else respuesta.Totales.MujeresSin++;
//            }
//            else if (p.Genero == 2)
//            {
//                if (idAlerta == 1) respuesta.Totales.HombresAlerta1++;
//                else if (idAlerta == 2) respuesta.Totales.HombresAlerta2++;
//                else respuesta.Totales.HombresSin++;
//            }
//        }

//        return Ok(respuesta);
//    }
//}
