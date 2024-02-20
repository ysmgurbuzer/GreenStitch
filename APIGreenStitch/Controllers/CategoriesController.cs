using BusinessLayer.Abstract;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGreenStitch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
                _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> ListCategory()
        {
            var categories = await _categoryService.GetListAsync();
            if (categories.Succeeded) { return Ok(categories); }
            else { return BadRequest(); }

        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryTitle(int categoryId)
        {
            var title = await _categoryService.GetByIdAsync<CategoryListDto>(categoryId);
            if (title.Succeeded) { return Ok(title); }  else { return BadRequest(); }
        }
    }
}
