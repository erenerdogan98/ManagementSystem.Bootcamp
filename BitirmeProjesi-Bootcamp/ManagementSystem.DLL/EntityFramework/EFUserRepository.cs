using ManagementSystem.DLL.Abstract;
using ManagementSystem.DLL.Database;
using ManagementSystem.DLL.Repository;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DLL.EntityFramework
{
    public class EFUserRepository : GenericRepository<User>, IUserDAL
    {
        public User Login(string email, string password)
        {
            using var c = new Context();
            return c.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
