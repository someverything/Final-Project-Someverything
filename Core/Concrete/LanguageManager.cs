using Core.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class LanguageManager : ILangService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string CurrentLanguageCode 
        {
            get
            {
                return _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault() ?? "az";
            }   
        }
    }
}
