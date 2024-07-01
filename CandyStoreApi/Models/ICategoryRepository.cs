namespace CandyStoreApi.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> AllCategories();
    }
}
