using RetoHemoglobina.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoHemoglobina.Application.DTOs
{
    public class PacienteDTO
    {
        public int Identificacion { get; set; }
        public string Nombre { get; set; }
        public byte GeneroId { get; set; }
    }
}