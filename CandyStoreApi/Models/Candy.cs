using System.Text.Json.Serialization;

namespace CandyStoreApi.Models
{
    public class Candy
    {
        public int CandyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public long Inventory {  get; set; }
        public int CategoryID { get; set; }
        [JsonIgnore]
        public Category Category { get; set; } = default!;

    }
}
