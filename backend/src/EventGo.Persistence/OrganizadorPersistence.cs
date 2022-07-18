using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventGo.Persistence
{
    public class OrganizadorPersistence : Contracts.IOrganizadorPersistence
    {
        private readonly EventGoContext _context;
        public OrganizadorPersistence(EventGoContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public async Task<Organizador[]> GetAllOrganizadoresAsync(bool includeEventos = false)
        {
            IQueryable<Organizador> query = _context.Organizadores
                .Include(o => o.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(o => o.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Evento);
            }

            query = query.OrderBy(o => o.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Organizador[]> GetAllOrganizadoresByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Organizador> query = _context.Organizadores
                .Include(o => o.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(o => o.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Evento);
            }

            query = query.OrderBy(o => o.Id)
                    .Where(o => o.Nome.ToLower()
                    .Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Organizador> GetOrganizadorByIdAsync(int organizadorId, bool includeEventos = false)
        {
            IQueryable<Organizador> query = _context.Organizadores
                .Include(o => o.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(o => o.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Evento);
            }

            query = query.OrderBy(o => o.Id)
                    .Where(o => o.Id == organizadorId);

            return await query.FirstOrDefaultAsync();
        }
    }
}