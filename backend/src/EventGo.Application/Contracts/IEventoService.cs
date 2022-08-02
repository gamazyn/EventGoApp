using System.Threading.Tasks;
using EventGo.Application.Dtos;


namespace EventGo.Application.Contracts
{
    public interface IEventoService
    {
        Task<EventoDTO> AddEventos(int userId, EventoDTO model);
        Task<EventoDTO> UpdateEvento(int userId, int eventoId, EventoDTO model);
        Task<bool> DeleteEvento(int userId, int eventoId);
        Task<EventoDTO[]> GetAllEventosAsync(int userId, bool includeOrganizadores = false);
        Task<EventoDTO[]> GetAllEventosByTemaAsync(int userId, string tema, bool includeOrganizadores = false);
        Task<EventoDTO> GetEventoByIdAsync(int userId, int eventoId, bool includeOrganizadores = false);
    }
}