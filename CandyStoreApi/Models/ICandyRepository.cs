namespace CandyStoreApi.Models
{
    public interface ICandyRepository
    {
        Task<IEnumerable<Candy>> AllCandies(string? category);
        Task<Candy?> GetCandyById(int candyId);
        Task<IEnumerable<Candy>> SearchCandies(string searchQuery);
    }
}
