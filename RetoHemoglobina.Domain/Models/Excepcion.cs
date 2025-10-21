using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoHemoglobina.Domain.Models;

//Modelo para manejar las excepciones de la aplicación y retornarlas al cliente
public class Excepcion
    {
        public int  Id { get; set; }
        public required string Mensaje { get; set; }
    }

