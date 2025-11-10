using Microsoft.EntityFrameworkCore;
using RetoHemoglobina.Application.DTOs;
using RetoHemoglobina.Application.Intefaces;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;
        private readonly IPacienteRepository _pacienteRepository;

        public ConsultaRepository(AppDbContext context, IPacienteRepository pacienteRepository)
        {
            _context = context;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<List<ResultadoPacienteDTO>> ListarConsultasAsync()
        {
            return await _context.Consultas
               .Select(m => new ResultadoPacienteDTO
               {
                   Nombre = m.Paciente.Nombre,
                   Identificacion = m.Identificacion,
                   Nivel = m.Nivel,
                   IdAlerta = m.AlertaId,
                   Alerta = m.Alerta.TipoAlerta,
                   Genero = m.Paciente.GeneroId,
               })
               .ToListAsync();
        }

        public async Task<List<ResultadoPacienteDTO>> ListarConsultasPorPacienteAsync(int identificacion)
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

        public async Task RegistrarConsultaAsync(ResultadoPacienteDTO datos)
        {
            //var paciente = await _pacienteRepository.ObtenerPorIdentificacionAsync(datos.Identificacion);

            //if (paciente == null)
            //{
            //    Paciente nuevoPaciente = new Paciente
            //    {
            //        Identificacion = datos.Identificacion,
            //        GeneroId = datos.Genero,
            //        Nombre = datos.Nombre
            //    };
            //    await _pacienteRepository.RegistrarPacienteAsync(nuevoPaciente);
            //}

            ResultadoPaciente consulta = new ResultadoPaciente
            {
                Identificacion = datos.Identificacion,
                Nivel = datos.Nivel,
                AlertaId = datos.IdAlerta,
            };

            _context.Consultas.Add(consulta);
            _context.SaveChanges();
        }
    }
}