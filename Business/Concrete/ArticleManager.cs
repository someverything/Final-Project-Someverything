using AutoMapper;
using Business.Abstract;
using Core.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs.ArticleDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                return new ErrorResult("An error occurred while creating articles.", false, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteAsync(Guid Id, string langCode)
        {
            try
            {
                _logger.LogInformation($"Delete method starts fucking fifth times");

                using var context = new AppDbContext();

                var article = context.Articles
                    .Include(a => a.ArtSubCats)
                        .ThenInclude(asc => asc.SubCategory)
                    .Include(a => a.ArticlePhotos)
                    .Include(a => a.ArticleTags)
                        .ThenInclude(at => at.Tag)
                    .FirstOrDefault(a => a.Id == Id);

                if (article is null)
                {
                    _logger.LogError($"Article with Id {Id} not found");
                    return new ErrorResult("Article not found", false, System.Net.HttpStatusCode.NotFound);
                }

                var articleLangs = _articleDAL.GetArticleLangs(Id);
                if (articleLangs is null || !articleLangs.Any())
                {
                    _logger.LogWarning($"No languages found for article with Id {Id}.");
                }
                else
                {
                    var articleLangEntities = context.ArticleLangs
                        .Where(al => al.ArticleId == Id)
                        .ToList();
                    context.ArticleLangs.RemoveRange(articleLangEntities);
                }

                context.ArticlePhotos.RemoveRange(article.ArticlePhotos);
                context.ArticleTags.RemoveRange(article.ArticleTags);
                context.ArtSubCats.RemoveRange(article.ArtSubCats);

                context.Articles.Remove(article);
                await context.SaveChangesAsync();

                _logger.LogInformation($"Article with Id {Id} and its related data deleted successfully.");
                return new SuccessResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)                                                                                                                                                                                                                                                                                                                                                                                                        
            {
                _logger.LogError(ex, $"Error occurred while deleting article with Id {Id}.");
                return new ErrorResult("An error occurred while deleting the article.", false, System.Net.HttpStatusCode.InternalServerError);
            }
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
