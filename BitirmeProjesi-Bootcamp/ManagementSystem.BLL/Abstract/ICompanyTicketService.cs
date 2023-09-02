using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL.Abstract
{
    public interface ICompanyTicketService
    {
        void AddTicketCompany(Company company);
        void UpdateTicketCompany(Company company);
        void DeleteTicketCompany(Company company);
        List<Company> GetAllTicketCompanies();
        Company GetTicketCompany(int id);
    }
}
