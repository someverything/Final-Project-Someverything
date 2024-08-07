using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;
using Entities.DTOs.TagDTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TagManager : ITagService
    {
        private readonly ITagDAL _tagDAL;
        private readonly IMapper _mapper;
        private readonly ILogger<SubCatManager> _logger;

        public TagManager(ITagDAL tagDAL, IMapper mapper, ILogger<SubCatManager> logger)
        {
            _tagDAL = tagDAL;
            _mapper = mapper;
            _logger = logger;
        }

        public IResult Create(CreateTagDTO model)
        {
            try
            {
                _tagDAL.Add(new()
                {
                    Name = model.Name
                });
                _logger.LogInformation("Tag successfyully added.");
                return new SuccessResult("Successfully added", true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding Tag.");
                return new ErrorResult(ex.Message, false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id)
        {
            var tag = await _tagDAL.GetTagAsync(Id);
            if (tag == null)
            {
                _logger.LogError("Tag with Id {Id} not found", Id);
                return new ErrorResult("Tah not found!", false, System.Net.HttpStatusCode.NotFound);
            }

            await _tagDAL.DeleteTag(tag.Id);
            _logger.LogInformation("Tag with Id { Id} deleted successfully.",Id);
            return new SuccessResult("Successfully deleted!", true, System.Net.HttpStatusCode.OK);

        }

        public IDataResult<IQueryable<GetTagDTO>> GetAll()
        {
            try
            {
                var tags = _tagDAL.GetAll();
                var tagDTOs = tags.Select(tag => _mapper.Map<GetTagDTO>(tag));

                return new SuccessDataResult<IQueryable<GetTagDTO>>(tagDTOs.AsQueryable(), System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorDataResults<IQueryable<GetTagDTO>>("An error occurred while retrieving tags.", System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<IResult> GetAsync(Guid Id)
        {
            try
            {
                var tag = await _tagDAL.GetTagAsync(Id);
                if(tag == null)
                {
                    _logger.LogError("Tag not found!", Id);
                    return new ErrorDataResults<GetTagDTO>("Tag not found!", System.Net.HttpStatusCode.NotFound);
                } 
                    
                var tagDTO = _mapper.Map<GetTagDTO>(tag);
                return new SuccessDataResult<GetTagDTO>(tagDTO, System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving the tag.", Id);
                return new ErrorDataResults<GetTagDTO>("An error occurred while retrieving the tag.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> Update(UpdateTagDTO model)
        {
            try
            {
                var tag = await _tagDAL.GetTagAsync(model.Id);
                if (tag is null)
                {
                    return new ErrorDataResults<GetTagDTO>("Tag not found!", System.Net.HttpStatusCode.NotFound);
                }
                _mapper.Map(model, tag);
                await _tagDAL.UpdateTagAsync(tag);
                return new SuccessDataResult<GetTagDTO>("Tag updated successfully!", System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult("An error occurred while updating the tag.", false, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
