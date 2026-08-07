namespace Application.DTOs
{
    public class ProductResponseDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int QuantityStock { get; set; }
    }
}
