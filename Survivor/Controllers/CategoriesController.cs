using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Data;
using Survivor.Dto;
using Survivor.Models;

namespace Survivor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly SurvivorContext _context;
        public CategoriesController(SurvivorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>>Get()
        {
            var categories = await _context.Categories
                .Where(c=>!c.IsDeleted).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>>GetById(int id)
        {
            var category = await _context.Categories
                .Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return NotFound($"{id}'li kategori bulunamadı");

            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<Category>>Create(CategoryCreateDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };
            _context.Categories.Add(category);  
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id=category.Id},category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id,CategoryUpdateDto categoryDto)
        {
            var currentCategory = await _context.Categories.Where(c=>!c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (currentCategory is null)
                return NotFound($"{id}'li kategori bulunamadı");

            currentCategory.Name = categoryDto.Name;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Kategori başarıyla güncellendi",
                NewName = categoryDto.Name
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
                return NotFound($"{id}'li kategori bulunamadı");

            category.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Kategori başarıyla silindi.",
                DeletedCategory = category
            });
        }
    }
}
