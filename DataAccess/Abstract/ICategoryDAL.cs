using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        void CreateCategory(List<AddCategoryDTO> models);
        Task UpdateCategoryAsync(Guid Id, List<UpdateCategoryDTO> models);
        void DeleteCategory(Guid Id);
        GetCategoryDTO GetCategoryByLang(Guid Id, string LangCode);
        List<GetCategoryDTO> GetAllCategories();
    }
}
