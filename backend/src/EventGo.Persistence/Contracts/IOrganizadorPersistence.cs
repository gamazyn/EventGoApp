using System.Threading.Tasks;
using EventGo.Domain;

namespace EventGo.Persistence.Contracts
{
    public interface IOrganizadorPersistence
    {
        //Comandos Organizadores
        Task<Organizador[]> GetAllOrganizadoresByNomeAsync(string nome, bool includeEventos);
        Task<Organizador[]> GetAllOrganizadoresAsync(bool includeEventos);
        Task<Organizador> GetOrganizadorByIdAsync(int organizadorId, bool includeEventos);
    }
}