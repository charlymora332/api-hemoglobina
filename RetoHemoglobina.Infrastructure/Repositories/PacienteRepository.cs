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

        public async Task<List<PacienteDTO>> ListarPacientesAsync()
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

        public async Task<Paciente?> ObtenerPorIdentificacionAsync(int identificacion)
        {
            return await _context.Paciente
                .FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        //}

        public async Task RegistrarPacienteAsync(Paciente paciente)
        {
            await _context.Paciente.AddAsync(paciente);
            await _context.SaveChangesAsync();
        }
    }
}