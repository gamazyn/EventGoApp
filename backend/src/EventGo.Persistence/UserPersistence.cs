using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGo.Domain.Identity;
using EventGo.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EventGo.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersistence
    {
        private readonly EventGoContext _context;
        public UserPersistence(EventGoContext context) : base(context)
        {
            _context = context;

        }
        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserbyUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName.ToLower());
        }
    }
}