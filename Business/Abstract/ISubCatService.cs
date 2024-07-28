using Core.Utilities.Results.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubCatService
    {
        IResult Create(CreateSubCatDTO model);
        IResult Update(Guid Id,UpdateSubCatDTO model);
        Task<IResult> DeleteAsync(Guid Id);
        IResult Get(Guid Id);
        IDataResult<List<GetSubCatDTO>> GetAll();
    }
}
