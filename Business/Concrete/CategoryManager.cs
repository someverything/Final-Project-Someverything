using Business.Abstract;
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
        private readonly ICategoryDAL

        public void Create(List<AddCategoryDTO> models)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<GetCategoryDTO> GetAllByLang(string langCode)
        {
            throw new NotImplementedException();
        }

        public GetCategoryDTO GetByLang(Guid id, string langCode)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, List<UpdateCategoryDTO> models)
        {
            throw new NotImplementedException();
        }
    }
}
