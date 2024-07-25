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

        public Task<CreateSubCatDTO> CreateAsync(CreateSubCatDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            var subCats = await _context.SubCategories.ToListAsync();
            return _mapper.Map<IEnumerable<SubCategory>>(subCats);
        }

        public async Task<GetSubCatDTO> GetAsync(int Id)
        {
            var subCat = await _context.SubCategories.FindAsync(Id);
            return _mapper.Map<GetSubCatDTO>(subCat);
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
