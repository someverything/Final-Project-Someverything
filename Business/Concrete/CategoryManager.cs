using Business.Abstract;
using Core.Abstract;
using DataAccess.Abstract;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private readonly ICategoryDAL _categoryDAL;
        private readonly ICategoryLangDAL _langDAL;
        private readonly ILangService _langService;

        public CategoryManager(ICategoryDAL categoryDAL, ICategoryLangDAL langDAL, ILangService langService)
        {
            _categoryDAL = categoryDAL;
            _langDAL = langDAL;
            _langService = langService;
        }

        public void Create(List<AddCategoryDTO> models)
        {
            _categoryDAL.CreateCategory(models);
        }

        public async Task Update(Guid id, List<UpdateCategoryDTO> models)
        {
            await _categoryDAL.UpdateCategoryAsync(id, models);
        }

        public void Delete(Guid id)
        {
            _categoryDAL.DeleteCategory(id);
        }

        public GetCategoryDTO GetByLang(Guid id, string langCode)
        {
            var CurrentLangCode = _langService.CurrentLanguageCode;
            var category = _langDAL.Get(x => x.CategoryId == id && x.LangCode == CurrentLangCode, false);
            if (category == null) return null;
            var result = new GetCategoryDTO()
            {
                CategoryId = category.CategoryId,
                LangCode = CurrentLangCode,
                Name = category.Name,
            };
            return result;
        }

        public List<GetCategoryDTO> GetAllByLang(string langCode)
        {
            var CurrentlangCode = _langService.CurrentLanguageCode;
            var categories = _langDAL.GetAll(x => x.LangCode == CurrentlangCode, false);
            var result = categories.Select(x => new GetCategoryDTO()
            {
                LangCode = x.LangCode,
                Name = x.Name,
                CategoryId = x.CategoryId
            }).ToList();

            return result;
        }



        
    }
}
