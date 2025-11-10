using Microsoft.EntityFrameworkCore;
using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Application.Intefaces;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResultadoPacienteDTO>> ListarConsultas()
        {
            return await _context.Consultas
               // .Where(m => m.Disponible && m.Aprobado)

               .Select(m => new ResultadoPacienteDTO
               {
                   Nombre = m.Paciente.Nombre,
                   Identificacion = m.Identificacion,
                   Nivel = m.Nivel,
                   IdAlerta = m.AlertaId,
                   Alerta = m.Alerta.TipoAlerta,
                   Genero = m.Paciente.GeneroId, // ← Trae el nombre del género
               })
               .ToListAsync();
        }

        public async Task<List<ResultadoPacienteDTO>> ListarConsultasPorPaciente(int identificacion)
        {
            return await _context.Consultas
               .Where(m => m.Identificacion == identificacion)
               .Select(m => new ResultadoPacienteDTO
               {
                   Nombre = m.Paciente.Nombre,
                   Identificacion = m.Identificacion,
                   Nivel = m.Nivel,
                   IdAlerta = m.AlertaId,
                   Alerta = m.Alerta.TipoAlerta,
                   Genero = m.Paciente.GeneroId, // ← Trae el nombre del género
               })
               .ToListAsync();
        }


        public async Task<List<PacienteDTO>> ListarPacientes()
        {
            return await _context.Paciente
               // .Where(m => m.Disponible && m.Aprobado)

               .Select(m => new PacienteDTO
               {
                   Nombre = m.Nombre,
                   Identificacion = m.Identificacion,
                   GeneroId = m.GeneroId,
               })
               .ToListAsync();
        }

        public void RegistrarConsultaR(ResultadoPacienteDTO resultadoPaciente)
        {
            try
            {
                Paciente paciente = new Paciente
                {
                    Identificacion = resultadoPaciente.Identificacion,
                    GeneroId = resultadoPaciente.Genero,
                    Nombre = resultadoPaciente.Nombre
                };
                ResultadoPaciente consulta = new ResultadoPaciente
                {
                    Identificacion = resultadoPaciente.Identificacion,
                    Nivel = resultadoPaciente.Nivel,
                    AlertaId = resultadoPaciente.IdAlerta,
                };

                int id = resultadoPaciente.Identificacion;

                if (_context.Paciente.Find(id) == null)
                {
                    RegistrarPacienteR(paciente);
                }

                RegistrarPacienteConsulta(consulta);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener consultas desde la base de datos.", ex);
            }
        }

        private void RegistrarPacienteR(Paciente paciente)
        {
            _context.Paciente.Add(paciente);
            _context.SaveChanges();
        }

        private void RegistrarPacienteConsulta(ResultadoPaciente consulta)
        {
            _context.Consultas.Add(consulta);
            _context.SaveChanges();
        }
    }
}