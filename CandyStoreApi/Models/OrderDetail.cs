namespace CandyStoreApi.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId {  get; set; }
        public int CandyId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Candy Candy { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
