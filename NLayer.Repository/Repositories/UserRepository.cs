using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public User  GetByUsername(string mail)
        {
            return _context.Users.FirstOrDefault(x => x.Email == mail);  
        }

        public bool UniqueEmail(string text)
        {
            bool s = !_context.Users.Any(u => u.Email.Trim().ToLower() == text.Trim().ToLower());
            return s;
        }
        public bool UniqueUsername(string text)
        {
            bool s = !_context.Users.Any(u => u.Email.Trim().ToLower() == text.Trim().ToLower());
            return s;
        }

    }
}
