using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.Services;
using ManagementSystem.BLL.ValidationRules;
using ManagementSystem.DLL.Abstract;
using ManagementSystem.DLL.Database;
using ManagementSystem.DLL.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.BLL
{
    public static class EFContextBLL
    {
        public static void AddScopeBLL(this IServiceCollection services)
        {
            services.AddScopeDAL();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserRegistrationValidator>();
            services.AddScoped<UserLoginValidator>();
            services.AddScoped<UserUpdateValidation>();

            services.AddDbContext<Context>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<EventCreateValidator>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<CategoryAddValidator>();

            services.AddScoped<ICityService, CityService>();
            services.AddScoped<CityAddValidator>();

            services.AddScoped<ICompanyTicketService, CompanyTicketService>();
            services.AddScoped<CompanyTicketValidator>();

            services.AddScoped<ITicketForCompaniesService, TicketForCompaniesService>();
            services.AddScoped<TicketValidator>();

        }
    }
}
