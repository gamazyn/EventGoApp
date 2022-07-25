using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain.Identity;

namespace EventGo.Persistence.Contracts
{
    public interface IUserPersistence : IGeralPersistence
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserbyUsernameAsync(string username);
    }
}