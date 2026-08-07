namespace Application.DTOs
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int QuantityStock { get; set; }
    }
}
