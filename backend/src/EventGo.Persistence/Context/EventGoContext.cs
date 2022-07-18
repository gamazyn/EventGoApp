using Microsoft.EntityFrameworkCore;
using EventGo.Domain;

namespace EventGo.Persistence
{
    public class EventGoContext : DbContext
    {
        public EventGoContext(DbContextOptions<EventGoContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<OrganizadorEvento> OrganizadoresEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrganizadorEvento>()
            .HasKey(OE => new { OE.EventoId, OE.OrganizadorId });

            builder.Entity<Evento>()
                .HasMany(e => e.RedeSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Organizador>()
            .HasMany(e => e.RedeSociais)
            .WithOne(rs => rs.Organizador)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}