using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandyStoreApi.Models;


namespace CadyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandiesController : ControllerBase
    {
        private readonly CandyStoreApiContext _context;
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CandiesController(CandyStoreApiContext context, ICandyRepository candyRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: api/Candies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candy>>> GetCandies(string? category)
        {
            IEnumerable<Candy> candies;
 
            candies = await _candyRepository.AllCandies(category);                

            return Ok(candies);
        }

        // GET: api/Candies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candy>> GetCandy(int id)
        {
            var candy = await _candyRepository.GetCandyById(id);

            if (candy == null)
            {
                return NotFound();
            }

            return Ok(candy);
        }

        // PUT: api/Candies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandy(int id, CandyDTO candyDTO)
        {
            Category? category = _categoryRepository.GetCategoryById(candyDTO.CategoryID);
            var candy = await _candyRepository.GetCandyById(id);
            if (category == null || (candy != null && id != candy.CandyId))
            {
                BadRequest();
            }
            if (candy == null)
            {
                NotFound();
            }
            else
            {
                candy.Name = candyDTO.Name;
                candy.Description = candyDTO.Description;
                candy.Price = candyDTO.Price;
                candy.ImageURL = candyDTO.ImageURL;
                candy.Inventory = candyDTO.Inventory;
                candy.CategoryID = candyDTO.CategoryID;

                _context.Entry(candy).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Candies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CandyDTO>> PostCandy(CandyDTO candyDTO)
        {
            Category? category = _categoryRepository.GetCategoryById(candyDTO.CategoryID);
            if (category == null) 
            { 
                BadRequest();
            }

            var candy = new Candy
            {
                Name = candyDTO.Name,
                Description = candyDTO.Description,
                Price = candyDTO.Price,
                ImageURL = candyDTO.ImageURL,
                Inventory = candyDTO.Inventory,
                CategoryID = candyDTO.CategoryID,
                Category = category!
            };
            
            _context.Candies.Add(candy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandy", new { id = candy.CandyId }, candy);
        }

        // DELETE: api/Candies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandy(int id)
        {
            var candy = await _context.Candies.FindAsync(id);
            if (candy == null)
            {
                return NotFound();
            }

            _context.Candies.Remove(candy);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CandyExists(int id)
        {
            return _context.Candies.Any(e => e.CandyId == id);
        }
    }
}
