using Core.Abstract;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        private readonly ILangService _langService;

        public EFCategoryDAL(ILangService langService)
        {
            _langService = langService;
        }

        public void CreateCategory(List<AddCategoryDTO> models)
        {
            using var context = new AppDbContext();
            Category category = new Category();
            context.Categories.Add(category);
            context.SaveChanges();

            for (int i = 0; i < models.Count; i++)
            {
                CategoryLang categoryLang = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CreatedDate = DateTime.Now,
                    CategoryId = category.Id
                };
                context.CategoryLangs.Add(categoryLang);
            }
            context.SaveChanges();
        }

        public void DeleteCategory(Guid Id)
        {
            using var context = new AppDbContext();
            var category = context.Categories.AsNoTracking().FirstOrDefault(x => x.Id == Id);

            if (category != null)
            {
                var categoryLangs = context.CategoryLangs.Where(x => x.CategoryId == Id).ToList();

                context.Categories.Remove(category);
                context.CategoryLangs.RemoveRange(categoryLangs);

                context.SaveChanges();
            }
        }

        public List<GetCategoryDTO> GetAllCategories()
        {
            using var context = new AppDbContext();
            var langCode = _langService.CurrentLanguageCode;
            var categories = context.CategoryLangs.AsNoTracking().Where(x => x.LangCode == langCode).ToList();
            var result = categories.Select(x => new GetCategoryDTO
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                LangCode = x.LangCode
            }).ToList();

            return result;
        }

        public GetCategoryDTO GetCategoryByLang(Guid Id, string LangCode)
        {
            using var context = new AppDbContext();
            var category = context.CategoryLangs.AsNoTracking()
                .FirstOrDefault(x => x.CategoryId == Id && x.LangCode == LangCode);

            GetCategoryDTO getCategoryDTO = new()
            {
                CategoryId = Id,
                LangCode = LangCode,
                Name = category.Name
            };

            return getCategoryDTO;
        }

        public async Task UpdateCategoryAsync(Guid Id, List<UpdateCategoryDTO> models)
        {
            using var context = new AppDbContext();
            var category = context.Categories.FirstOrDefault(x => x.Id == Id);
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            var categoryLang = context.CategoryLangs.Where(x => x.CategoryId == category.Id).ToList();

            context.CategoryLangs.RemoveRange(categoryLang);
            for (int i = 0; i < models.Count; i++)
            {
                CategoryLang newCategoryLang = new()
                {
                    Name = models[i].Name,
                    LangCode = models[i].LangCode,
                    CategoryId = category.Id
                };
                await context.CategoryLangs.AddAsync(newCategoryLang);
            }
            await context.SaveChangesAsync();
        }
    }
}
