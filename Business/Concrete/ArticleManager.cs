using AutoMapper;
using Business.Abstract;
using Core.Abstract;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
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

        public Task<IResult> CreateAsync(List<AddArticleDTO> models)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid Id)
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
