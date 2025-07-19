using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Data;
using Survivor.Dto;
using Survivor.Models;

namespace Survivor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitorsController : ControllerBase
    {
        
        private readonly SurvivorContext _context;

        public CompetitorsController(SurvivorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competitor>>> Get()
        {
            var competitors = await _context.Competitors.Where(c=>!c.IsDeleted).ToListAsync() ;
            return Ok(competitors);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Competitor>> GetById(int id)
        {
            var competitor = await _context.Competitors.Where(c=>!c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
            if (competitor is null)
                return NotFound($"Girilen {id} 'li kullanıcı bulunamadı");

            return Ok(competitor);
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Competitor>>> GetByCategoryId(int categoryId)
        {
            var competitors=await _context.Competitors
                .Where(c => !c.IsDeleted && c.CategoryId==categoryId)
                .ToListAsync();

            return Ok(competitors);
        }
        [HttpPost]
        public async Task<ActionResult<Competitor>> Create(CompetitorCreateDto competitorDto)
        {
            var competitor = new Competitor
            {
                FirstName = competitorDto.FirstName,
                LastName = competitorDto.LastName,
                CategoryId = competitorDto.CategoryId,
            };
            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = competitor.Id }, competitor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Competitor>>UpdateCompetitor(int id, CompetitorUpdateDto competitorDto)
        {
            var currentCompetitor = await _context.Competitors
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (currentCompetitor is null)
                return NotFound($"{id}'li yarışmacı bulunamadı");

            
            currentCompetitor.FirstName = competitorDto.FirstName;
            currentCompetitor.LastName = competitorDto.LastName;
            currentCompetitor.CategoryId = competitorDto.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Yarışmacı güncellendi",
                NewFirstName = competitorDto.FirstName,
                NewLastName = competitorDto.LastName,
                NewCategoryId = competitorDto.CategoryId,
            });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Competitor>>Delete(int id)
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(c => c.Id == id);

            if (competitor is null)
                return NotFound($"{id}'li yarışmacı bulunamadı");

            if(competitor.IsDeleted==true)
                return BadRequest($"{id}'li yarışmacı zaten silinmiş.");


            competitor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Yarışmacı silindi",
                DeletedCompetitor = competitor
            });
        }
    }
}
