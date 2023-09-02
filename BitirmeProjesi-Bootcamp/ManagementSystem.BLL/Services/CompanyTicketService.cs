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
    public class CompanyTicketService : ICompanyTicketService
    {
        EFCompanyRepository _efcompanyrepository;
        public CompanyTicketService()
        {
            _efcompanyrepository = new EFCompanyRepository();
        }
        public void AddTicketCompany(Company company)
        {
            _efcompanyrepository.Insert(company);
        }

        public void DeleteTicketCompany(Company company)
        {
            _efcompanyrepository.Delete(company);
        }

        public List<Company> GetAllTicketCompanies()
        {
           return _efcompanyrepository.GetAll();
        }

        public Company GetTicketCompany(int id)
        {
            return _efcompanyrepository.GetByID(id);
        }

        public void UpdateTicketCompany(Company company)
        {
            _efcompanyrepository.Update(company);
        }     
    }
}
