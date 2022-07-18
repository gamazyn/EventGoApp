using System.Threading.Tasks;

namespace EventGo.Persistence.Contracts
{
    public interface IGeralPersistence
    {
        //Comandos Gerais
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}