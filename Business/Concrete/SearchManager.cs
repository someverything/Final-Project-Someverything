using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.DTOs.SearchDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SearchManager : ISearchService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly ICategoryLangDAL _langDAL;
        private readonly ISubCategoryDAL _subCategoryDAL;
        private readonly UserManager<AppUser> _userManager;

        public SearchManager(ICategoryDAL categoryDAL, ICategoryLangDAL langDAL, ISubCategoryDAL subCategoryDAL, UserManager<AppUser> userManager)
        {
            _categoryDAL = categoryDAL;
            _langDAL = langDAL;
            _subCategoryDAL = subCategoryDAL;
            _userManager = userManager;
        }

        public IDataResult<List<SearchResDTO>> SearchByCriteries(SearchCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}
