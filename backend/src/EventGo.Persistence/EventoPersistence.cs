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

        public async Task<Evento[]> GetAllEventosAsync(int userId, bool includeOrganizadores = false)
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

            query = query.Where(e => e.UserId == userId).OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includeOrganizadores = false)
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
                    .Where(e => e.Tema.ToLower().Contains(tema.ToLower()) &&
                                e.UserId == userId);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includeOrganizadores = false)
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
                    .Where(e => e.Id == eventoId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}