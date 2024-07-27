﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
