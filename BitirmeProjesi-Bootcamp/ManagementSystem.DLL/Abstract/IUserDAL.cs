using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DLL.Abstract
{
    public interface IUserDAL:IGenericDAL<User>
    {
        User Login(string email, string password);
    }
}
