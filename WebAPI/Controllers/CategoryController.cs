using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public IActionResult Create(List<AddCategoryDTO> models)
        {
            _categoryService.Create(models);
            return Ok();
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateAsync(Guid Id, List<UpdateCategoryDTO> models)
        {
            await _categoryService.Update(Id, models);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            _categoryService.Delete(Id);
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id, [FromQuery] string langCode)
        {
            if (string.IsNullOrEmpty(langCode))
            {
                return BadRequest("Language code is required!");
            }

            var result = _categoryService.GetByLang(Id, langCode);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] string langCode)
        {
            if (string.IsNullOrEmpty(langCode)) return BadRequest("Language code is required!");
            var result = _categoryService.GetAllByLang(langCode);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
