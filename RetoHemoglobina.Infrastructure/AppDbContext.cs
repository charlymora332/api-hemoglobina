using Microsoft.EntityFrameworkCore;
using RetoHemoglobina.Domain.Models;

namespace RetoHemoglobina.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<ResultadoPaciente> Consultas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Paciente>()
    .HasKey(p => p.Identificacion);

            modelBuilder.Entity<Paciente>()
                .Property(p => p.Identificacion)
                .ValueGeneratedNever(); // No se autoincrementa

            modelBuilder.Entity<ResultadoPaciente>().HasKey(r => r.Id);
            modelBuilder.Entity<Genero>().HasKey(g => g.Id);
            modelBuilder.Entity<Alerta>().HasKey(a => a.Id);

            // Relaciones
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Genero)
                .WithMany(g => g.Pacientes)
                .HasForeignKey(p => p.GeneroId);

            modelBuilder.Entity<ResultadoPaciente>()
                .HasOne(r => r.Paciente)
                .WithMany(p => p.Resultados)
                .HasForeignKey(r => r.Identificacion);

            modelBuilder.Entity<ResultadoPaciente>()
                .HasOne(r => r.Alerta)
                .WithMany(a => a.Resultados)
                .HasForeignKey(r => r.AlertaId);

            // 🧩 Datos semilla
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, TipoGenero = "Masculino" },
                new Genero { Id = 2, TipoGenero = "Femenino" }
            );

            modelBuilder.Entity<Alerta>().HasData(
                new Alerta { Id = 0, TipoAlerta = "Sin alerta" },
                new Alerta { Id = 1, TipoAlerta = "Nivel bajo" },
                new Alerta { Id = 2, TipoAlerta = "Nivel alto" }
            );

            modelBuilder.Entity<Paciente>().HasData(
                new Paciente { Identificacion = 101, Nombre = "Carlos Mora", GeneroId = 1 },
                new Paciente { Identificacion = 102, Nombre = "Laura Ríos", GeneroId = 2 }
            );

            modelBuilder.Entity<ResultadoPaciente>().HasData(
                new ResultadoPaciente { Id = 1, Identificacion = 101, Nivel = 14.5f, AlertaId = 0 },
                new ResultadoPaciente { Id = 2, Identificacion = 102, Nivel = 13.2f, AlertaId = 1 }
            );
        }
    }
}