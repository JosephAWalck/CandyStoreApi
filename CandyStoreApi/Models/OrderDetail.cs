using System.Text.Json.Serialization;

namespace CandyStoreApi.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId {  get; set; }
        public int CandyId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Candy Candy { get; set; } = default!;
        [JsonIgnore]
        public Order Order { get; set; } = default!;
    }
}
