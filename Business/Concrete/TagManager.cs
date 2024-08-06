using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
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
                    Id = model.Id,
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
            var tag = await _tagDAL.GetAsync(Id);
        }

        public IDataResult<IQueryable<GetSubCatDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(UpdateTagDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
