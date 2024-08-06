using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.Extensions.Logging;

namespace Business.Concrete
{
    public class SubCatManager : ISubCatService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;
        private readonly IMapper _mapper;
        private readonly ILogger<SubCatManager> _logger;

        public SubCatManager(ISubCategoryDAL subCategoryDAL, IMapper mapper, ILogger<SubCatManager> logger)
        {
            _subCategoryDAL = subCategoryDAL;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogInformation("SubCategory successfully added.");
                return new SuccessResult("Ugurla elave olundu!", true, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding SubCategory.");
                return new ErrorResult(ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            var subCat = await _subCategoryDAL.GetAsync(Id);
            if (subCat == null)
            {
                _logger.LogWarning("SubCategory with Id {Id} not found.", Id);
                return new ErrorResult("SubCategory not found!", false, System.Net.HttpStatusCode.NotFound);
            }

            await _subCategoryDAL.DeleteAsync(subCat);
            _logger.LogInformation("SubCategory with Id {Id} deleted successfully.", Id);
            return new SuccessResult("SubCategory deleted successfully!", true, System.Net.HttpStatusCode.OK);
        }


        public IDataResult<List<GetSubCatDTO>> GetAll()
        {
            try
            {
                var subCats = _subCategoryDAL.GetAll();
                var result = _mapper.Map<List<GetSubCatDTO>>(subCats);
                _logger.LogInformation("Retrieved all SubCategories.");
                return new DataResult<List<GetSubCatDTO>>(result, true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all SubCategories.");
                return new DataResult<List<GetSubCatDTO>>(null, false, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IResult> GetAsync(Guid Id)
        {
            var subCat = await _subCategoryDAL.GetAsync(Id);
            if (subCat is null)
            {
                _logger.LogWarning("SubCategory with Id {Id} not found.", Id);
                return new ErrorDataResults<GetSubCatDTO>("SubCategory not found", System.Net.HttpStatusCode.NotFound);
            }

            var result = new GetSubCatDTO
            {
                Name = subCat.Name,
                CategoryId = subCat.CategoryId,
                CreatedDate = subCat.CreatedDate,
                UpdatedDate = subCat.UpdatedDate
            };
            _logger.LogInformation("SubCategory with Id {Id} retrieved successfully.", Id);
            return new SuccessDataResult<GetSubCatDTO>(result, "SubCategory retrieved successfully", System.Net.HttpStatusCode.OK);
        }

        async Task<IResult> ISubCatService.Update(UpdateSubCatDTO model)
        {
            try
            {
                var updatedSubCat = await _subCategoryDAL.UpdateAsync(model);
                if (updatedSubCat is null)
                {
                    _logger.LogWarning("SubCategory with Id {Id} not found.", model.Id);
                    return new SuccessDataResult<UpdateSubCatDTO>(updatedSubCat, "SubCategory successfully updated", System.Net.HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating SubCategory with Id {Id}.", model.Id);
                return new ErrorDataResults<UpdateSubCatDTO>("SubCategory not found", System.Net.HttpStatusCode.NotFound);
            }
            return null;
        }
    }
}
