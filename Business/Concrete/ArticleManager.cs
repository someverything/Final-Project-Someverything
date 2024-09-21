using AutoMapper;
using Business.Abstract;
using Core.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs.ArticleDTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleServices
    {
        private readonly IArticleDAL _articleDAL;
        private readonly ILangService _langService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ArticleManager(IArticleDAL articleDAL, ILangService langService, ILogger logger, IMapper mapper)
        {
            _articleDAL = articleDAL;
            _langService = langService;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<IResult> CreateAsync(List<AddArticleDTO> models)
        {
            try
            {
                _logger.LogInformation("Starting to create Article");
                
                await _articleDAL.CreateArticleAsync(models);
                
                _logger.LogInformation("Article created successfully");
                return new SuccessResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ocured while creating article");
                return new ErrorResult("An error occurred while creating articles.", false,System.Net.HttpStatusCode.BadRequest);
            }
        }

        public Task<IResult> DeleteAsync(Guid Id, string langCode)
        {
            throw new NotImplementedException();
        }

        public GetArticleDTO Get(Guid Id, string langCode)
        {
            throw new NotImplementedException();
        }

        public ICollection<GetArticleDTO> GetAll(string langCode)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(Guid Id, List<AddArticleDTO> models)
        {
            throw new NotImplementedException();
        }
    }
}
