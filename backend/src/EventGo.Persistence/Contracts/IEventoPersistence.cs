using System.Threading.Tasks;
using EventGo.Domain;

namespace EventGo.Persistence.Contracts
{
    public interface IEventoPersistence
    {
        //Comandos Eventos
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includeOrganizadores = false);
        Task<Evento[]> GetAllEventosAsync(int userId, bool includeOrganizadores = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includeOrganizadores = false);
    }
}