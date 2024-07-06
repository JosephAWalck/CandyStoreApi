using Microsoft.EntityFrameworkCore;

namespace CandyStoreApi.Models
{
    public class CandyRepository : ICandyRepository
    {
        private readonly CandyStoreApiContext _candyStoreApiContext;

        public CandyRepository(CandyStoreApiContext candyStoreApiContext) 
        {
            _candyStoreApiContext = candyStoreApiContext;
        }
        public async Task<IEnumerable<Candy>> AllCandies(string? category = null) 
        {
            if (category == null) 
            {
                return await _candyStoreApiContext.Candies
                    .Include(c => c.Category)
                    .ToListAsync();
            }
            else 
            {
                return await _candyStoreApiContext.Candies
                    .Include(c => c.Category)
                    .Where(c => c.Category.CategoryName == category)
                    .ToListAsync();
            }
        }

        public async Task<Candy?> GetCandyById(int candyId)
        {
            return await _candyStoreApiContext.Candies.FirstOrDefaultAsync(c => c.CandyId == candyId);
            
        }

        public async Task<IEnumerable<Candy>> SearchCandies(string searchQuery)
        {
            // possibly bad practice?
            return await _candyStoreApiContext.Candies.Where(c => c.Name.Contains(searchQuery)).ToListAsync();
            
        }
    }
}
