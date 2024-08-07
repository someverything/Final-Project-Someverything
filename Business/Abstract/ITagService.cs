using Core.Utilities.Results.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using Entities.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITagService
    {
        IResult Create(CreateTagDTO model);
        Task<IResult> Update(UpdateTagDTO model);
        Task<IResult> DeleteAsync(Guid Id);
        Task<IResult> GetAsync(Guid Id);
        IDataResult<IQueryable<GetTagDTO>> GetAll();
    }
}
