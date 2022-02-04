using System.Threading.Tasks;
using System.Collections.Generic;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly ProEventosContext _context;

        public UserPersist(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users
                                .SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }
    }
}
