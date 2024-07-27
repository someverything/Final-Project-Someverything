using AutoMapper;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFSubCatDAL : EFRepositoryBase<SubCategory, AppDbContext>, ISubCategoryDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EFSubCatDAL(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateSubCatDTO> CreateAsync(CreateSubCatDTO model)
        {
            var subCategory = new SubCategory
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId
            };

            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateSubCatDTO>(model);
        }

        public async Task DeleteAsync(SubCategory subCategory)
        {
            _context.SubCategories.Remove(subCategory);
            await (_context.SaveChangesAsync());
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            var subCats = await _context.SubCategories.ToListAsync();
            return _mapper.Map<IEnumerable<SubCategory>>(subCats);
        }

        public async Task<SubCategory> GetAsync(Guid Id)
        {
            return await _context.SubCategories.FindAsync(Id);
        }

        public async Task<UpdateSubCatDTO> UpdateAsync(UpdateSubCatDTO model)
        {
            var subCat = await _context.SubCategories.FindAsync(model.Id);
            if (subCat == null) return null;
            _mapper.Map(model, subCat);
            await _context.SaveChangesAsync();
            return _mapper.Map<UpdateSubCatDTO>(subCat);
        }

    }
}
