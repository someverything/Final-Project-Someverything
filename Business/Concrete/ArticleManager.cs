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

        public IDataResult<GetArticleDTO> Get(Guid Id, string langCode)
        {
            var article = _articleDAL.GetArticle(Id, langCode);
            if (article is null)
            {
                _logger.LogError("There is no data exist");
                return new ErrorDataResults<GetArticleDTO>(null, "Article not found", false, System.Net.HttpStatusCode.NotFound);
            }

            var articleDTO = new GetArticleDTO
            {
                Id = article.Id,
                LangCode = article.LangCode,
                ArticleTags = article.ArticleTags,
                ArtSubCats = article.ArtSubCats,
                CreatedBy = article.CreatedBy,
                Description = article.Description,
                IsActive = article.IsActive,
                Photo = article.Photo,
                SubCategoryName = article.SubCategoryName,
                TagName = article.TagName,
                Title = article.Title,
                Views = article.Views
            };

            return new SuccessDataResult<GetArticleDTO>(articleDTO, "Article retrieved successfully", System.Net.HttpStatusCode.OK);

        }

        public IDataResult<ICollection<GetArticleDTO>> GetAll(string langCode)
        {
            var articles = _articleDAL.GetAllArticles(langCode);

            if (articles == null || !articles.Any())
            {
                _logger.LogError("No articles found for the given language code");
                return new ErrorDataResults<ICollection<GetArticleDTO>>("No articles found", System.Net.HttpStatusCode.NotFound);
            }

            var articleDTOs = articles.Select(article => new GetArticleDTO
            {
                Id=article.Id,
                LangCode = article.LangCode,
                ArticleTags = article.ArticleTags,
                ArtSubCats= article.ArtSubCats,
                CreatedBy = article.CreatedBy,
                Description = article.Description,
                IsActive = article.IsActive,
                Photo = article.Photo,
                SubCategoryName = article.SubCategoryName,
                TagName = article.TagName,
                Title = article.Title,
                Views = article.Views
            }).ToList();

            return new SuccessDataResult<ICollection<GetArticleDTO>>(articleDTOs, "Articles retrieved successfully", System.Net.HttpStatusCode.OK);
        }

        public Task<IResult> UpdateAsync(Guid Id, List<AddArticleDTO> models)
        {
            throw new NotImplementedException();
        }
    }
}
