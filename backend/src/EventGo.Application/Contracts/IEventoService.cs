using System.Threading.Tasks;
using EventGo.Application.Dtos;


namespace EventGo.Application.Contracts
{
    public interface IEventoService
    {
        Task<EventoDTO> AddEventos(EventoDTO model);
        Task<EventoDTO> UpdateEvento(int eventoId, EventoDTO model);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDTO[]> GetAllEventosAsync(bool includeOrganizadores = false);
        Task<EventoDTO[]> GetAllEventosByTemaAsync(string tema, bool includeOrganizadores = false);
        Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includeOrganizadores = false);
    }
}