using ManagementSystem.BLL.Abstract;
using ManagementSystem.DLL.EntityFramework;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Services
{
    public class CityService : ICityService
    {
        EFCityRepository _efcityrepository;
        public CityService()
        {
            _efcityrepository = new EFCityRepository();
        }
        public void AddCity(City city)
        {
            _efcityrepository.Insert(city);
        }

        public void DeleteCity(City city)
        {
            _efcityrepository.Delete(city);
        }

        public List<City> GetAll()
        {
            return _efcityrepository.GetAll();
        }

        public City GeyCityByID(int Cityid)
        {
            return _efcityrepository.GetByID(Cityid);
        }

        public void UpdateCity(City city)
        {
            _efcityrepository.Update(city);
        }
    }
}
