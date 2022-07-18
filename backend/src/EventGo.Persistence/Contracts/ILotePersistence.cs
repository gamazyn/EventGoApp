using System.Threading.Tasks;
using EventGo.Domain;

namespace EventGo.Persistence.Contracts
{
    public interface ILotePersistence
    {
        //Comandos Lotes
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}