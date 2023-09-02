using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Abstract
{
    public interface ICityService
    {
        void AddCity(City city);
        void UpdateCity(City city);
        void DeleteCity(City city);
        List<City> GetAll();
        City GeyCityByID(int Cityid);
    }
}
