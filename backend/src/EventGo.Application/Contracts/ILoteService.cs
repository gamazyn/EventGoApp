using System.Threading.Tasks;
using EventGo.Application.Dtos;


namespace EventGo.Application.Contracts
{
    public interface ILoteService
    {
        Task<LoteDTO[]> SaveLote(int eventoId, LoteDTO[] models);
        Task<bool> DeleteLote(int eventoId, int loteId);
        Task<LoteDTO[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDTO> GetLotebyIdsAsync(int eventoId, int loteIds);
    }
}