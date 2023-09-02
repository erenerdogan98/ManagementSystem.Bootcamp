using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.DLL.Database;
using ManagementSystem.DLL.EntityFramework;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Services
{
    public class UserService : IUserService
    {
        EFUserRepository eFUserRepository; // class seviyesinde çağırım , yapıcı metotumuzda new ile örnekleyeceğiz

        public UserService()
        {
            eFUserRepository = new EFUserRepository();
        }
        public void AddUser(User user)
        {
            eFUserRepository.Insert(user);
        }

        public void DeleteUser(User user)
        {
            eFUserRepository.Delete(user);
        }

        public List<User> GetAllUsers()
        {
            return eFUserRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return eFUserRepository.GetByID(id);
        }

        public User Login(string email, string password)
        {
            return eFUserRepository.Login(email, password);
        }

        public void UpdateUser(User user)
        {
            eFUserRepository.Update(user);
        }

    }
}
