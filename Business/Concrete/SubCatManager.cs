using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SubCatManager : ISubCatService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;
        private readonly IMapper _mapper;

        public SubCatManager(ISubCategoryDAL subCategoryDAL, IMapper mapper)
        {
            _subCategoryDAL = subCategoryDAL;
            _mapper = mapper;
        }

        public IResult Create(CreateSubCatDTO model)
        {
            try
            {
                _subCategoryDAL.Add(new()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name
                });
                return new SuccessResult("Ugurla elave olundu!", true, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            var subCat = await _subCategoryDAL.GetAsync(Id);
            if (subCat == null) return new ErrorResult("SubCategory not found!", false, System.Net.HttpStatusCode.NotFound);

            await _subCategoryDAL.DeleteAsync(subCat);
            return new SuccessResult("SubCategory deleted successfully!", true, System.Net.HttpStatusCode.OK);
        }

        public IResult Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<GetSubCatDTO>> GetAll(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Guid Id, UpdateSubCatDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
