using AutoMapper;
using Azure;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFTagDAL : EFRepositoryBase<Tag, AppDbContext>, ITagDAL
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EFTagDAL(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IDataResult<CreateTagDTO>> CreateTag(CreateTagDTO model)
        {
            try
            {
                Tag tag = _mapper.Map<Tag>(model);
                await _context.Tags.AddAsync(tag);
                await _context.SaveChangesAsync();
                return new SuccessDataResult<CreateTagDTO>(model, "Tag created successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return new ErrorDataResults<CreateTagDTO>("An error occurred while creating the tag.", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task DeleteTag(Guid Id)
        {
            var tag = await _context.Tags.FindAsync(Id);

            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Tag> GetAll()
        {
            var tags = _context.Tags.ToList();
            return _mapper.Map<IQueryable<Tag>>(tags);
        }

        public async Task<Tag> GetTagAsync(Guid Id)
        {
            var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<Tag>(tag);
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
