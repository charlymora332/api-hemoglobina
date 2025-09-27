using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SubcadenasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcadenasController : ControllerBase
    {
        /// <summary>
        /// Método que obtiene todas las subcadenas de una longitud dada
        /// y devuelve la más pequeña y la más grande en orden lexicográfico.
        /// </summary>
        /// <param name="cadena">La cadena original de texto</param>
        /// <param name="tamanio">El tamaño de cada subcadena</param>
        /// <returns>Lista de subcadenas + menor y mayor</returns>
        [HttpGet]
        public IActionResult ObtenerSubcadenas(string cadena, int tamanio)
        {
            // Validación básica de parámetros
            if (string.IsNullOrEmpty(cadena) || tamanio <= 0 || tamanio > cadena.Length)
            {
                return BadRequest("Parámetros inválidos.");
            }

            // Lista donde guardaremos todas las subcadenas
            List<string> listaSubcadenas = new List<string>();

            // Inicializamos las variables con la primera subcadena
            string masPequena = cadena.Substring(0, tamanio);
            string masGrande = cadena.Substring(0, tamanio);

            // Recorremos la cadena para sacar subcadenas de tamaño fijo
            for (int i = 0; i <= cadena.Length - tamanio; i++)
            {
                // Tomamos una subcadena desde i con longitud = tamanio
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
