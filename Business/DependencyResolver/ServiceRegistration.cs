using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBussinessServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();
            services.AddScoped<ICategoryLangDAL, EFCategoryLangDAL>();

            services.AddScoped<ISubCatService, SubCatManager>();
            services.AddScoped<ISubCategoryDAL, EFSubCatDAL>();
            
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ITagDAL, EFTagDAL>();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
