
namespace WebApplication1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int? CategoryID { get; set; }
        public string? Sku { get; set; }
        public string? ProductName { get; set; }
        public string? UnitName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
