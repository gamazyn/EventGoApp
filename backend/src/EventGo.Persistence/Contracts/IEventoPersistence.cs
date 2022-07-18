using System.Threading.Tasks;
using EventGo.Domain;

namespace EventGo.Persistence.Contracts
{
    public interface IEventoPersistence
    {
        //Comandos Eventos
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includeOrganizadores);
        Task<Evento[]> GetAllEventosAsync(bool includeOrganizadores);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includeOrganizadores);
    }
}