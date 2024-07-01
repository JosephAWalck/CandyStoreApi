using Microsoft.EntityFrameworkCore;

namespace CandyStoreApi.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CandyStoreApiContext _candyStoreApiContext;
        public CategoryRepository(CandyStoreApiContext candyStoreApiContext)
        {
            _candyStoreApiContext = candyStoreApiContext;
        }
        public async Task<IEnumerable<Category>> AllCategories()
        {
            {
                var task = Task.Run(() => _candyStoreApiContext.Categories.OrderBy(c => c.CategoryName));
                return await task;
            }
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _candyStoreApiContext.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
        }
    }
}
