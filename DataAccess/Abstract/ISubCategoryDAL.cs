using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISubCategoryDAL : IRepositoryBase<SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<GetSubCatDTO> GetAsync(int Id);
        Task<bool> DeleteAsync(int Id);
        Task<UpdateSubCatDTO> UpdateAsync(UpdateSubCatDTO model);
        Task<CreateSubCatDTO> CreateAsync(CreateSubCatDTO model);
    }
}
