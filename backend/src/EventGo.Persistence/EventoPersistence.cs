using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventGo.Persistence
{
    public class EventoPersistence : Contracts.IEventoPersistence
    {
        private readonly EventGoContext _context;
        public EventoPersistence(EventGoContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<Evento[]> GetAllEventosAsync(bool includeOrganizadores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includeOrganizadores)
            {
                query = query
                    .Include(e => e.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Organizador);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includeOrganizadores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includeOrganizadores)
            {
                query = query
                    .Include(e => e.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Organizador);
            }

            query = query.OrderBy(e => e.Id)
                    .Where(e => e.Tema.ToLower()
                    .Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includeOrganizadores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includeOrganizadores)
            {
                query = query
                    .Include(e => e.OrganizadoresEventos)
                    .ThenInclude(oe => oe.Organizador);
            }

            query = query.OrderBy(e => e.Id)
                    .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}