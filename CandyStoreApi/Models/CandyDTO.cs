namespace CandyStoreApi.Models
{
    public class CandyDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public long Inventory { get; set; }
        public int CategoryID { get; set; }
    }
}
