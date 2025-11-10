using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoHemoglobina.Application.Validators
{
    public static class PacienteValidacion
    {
        public static void Validar(PacienteRequestDTO p, int id)
        {
            var errores = new List<string>();

            if (p.Nivel <= 0 || p.Nivel > 30)
                errores.Add($"Nivel {p.Nivel} no válido para paciente {id} -> {p.Nombre}, debe ser >0 y <=30.");

            if (p.Identificacion == 0)
                errores.Add($"Número de identificación no válido para paciente {id} -> {p.Nombre}.");

            if (p.GeneroId != 1 && p.GeneroId != 2)
                errores.Add($"Género {p.GeneroId} no válido para paciente {id} -> {p.Nombre}.");

            if (errores.Any())
                throw new ValidacionException(errores);
        }
    }
}