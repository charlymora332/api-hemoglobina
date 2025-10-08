using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SubcadenasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcadenasController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObtenerSubcadenas(string cadena, int tamanio)
        {
            // Validacion tamanio no sea mayor al largo de la cadena
            if (string.IsNullOrEmpty(cadena) || tamanio <= 0 || tamanio > cadena.Length)
            {
                return BadRequest("Parámetros inválidos.");
            }

            
            List<string> listaSubcadenas = new List<string>();

            
            string masPequena = cadena.Substring(0, tamanio);
            string masGrande = cadena.Substring(0, tamanio);

            // Recorremos la cadena para sacar subcadenas de tamaño fijo
            for (int i = 0; i <= cadena.Length - tamanio; i++)
            {
       
                string subcadena = cadena.Substring(i, tamanio);

                // Guardamos cada subcadena en la lista
                listaSubcadenas.Add(subcadena);

                // Verificamos si es menor en orden lexicográfico
                if (string.Compare(subcadena, masPequena) < 0)
                {
                    masPequena = subcadena;
                }

                // Verificamos si es mayor en orden lexicográfico
                if (string.Compare(subcadena, masGrande) > 0)
                {
                    masGrande = subcadena;
                }
            }

            // Retornamos todas las subcadenas junto con la menor y mayor
            return Ok(new
            {
                Subcadenas = listaSubcadenas,
                Menor = masPequena,
                Mayor = masGrande
            });
        }
    }
}
