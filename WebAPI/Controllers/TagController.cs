using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using Entities.DTOs.TagDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("Create")]
        public IActionResult CreateTag([FromBody] CreateTagDTO model)
        {
            var result = _tagService.Create(model);
            if(result.Success) return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteTag(Guid Id)
        {
            var result = await _tagService.DeleteAsync(Id);
            if (result.Success) return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("Get/{Id}")]
        public async Task<IActionResult> GetAsync(Guid Id)
        {
            var result = await _tagService.GetAsync(Id);
            if (result.Success) return Ok(result);
            return BadRequest(result.Message);

        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _tagService.GetAll();
            if (result.Success) return Ok();
            return Ok(result);
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTagDTO model)
        {
            var result = await _tagService.Update(model);
            if (result.Success) return Ok(result);
            return StatusCode((int)result.StatusCode, result.Message);
        }

    }
}
