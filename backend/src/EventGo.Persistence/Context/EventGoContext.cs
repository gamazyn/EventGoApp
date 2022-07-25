using Microsoft.EntityFrameworkCore;
using EventGo.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EventGo.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EventGo.Persistence
{
    public class EventGoContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                                    UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public EventGoContext(DbContextOptions<EventGoContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<OrganizadorEvento> OrganizadoresEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                    userRole.HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired();

                    userRole.HasOne(ur => ur.User)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.UserId)
                            .IsRequired();
                }
            );

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