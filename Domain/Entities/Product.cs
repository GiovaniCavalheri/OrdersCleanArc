namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int QuantityStock { get; private set; }

        private Product() { }

        public Product(string productName, decimal productPrice, int quantityStock)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("--Criar Throw Exception---");
            if (productPrice < 0)
                throw new ArgumentException("--Criar Throw Exception---");
            if (quantityStock < 0)
                throw new ArgumentException("--Criar Throw Exception---");

            Id = Guid.NewGuid();
            ProductName = productName;
            ProductPrice = productPrice;
            QuantityStock = quantityStock;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("--Criar Throw Exception---");

            ProductPrice = newPrice;
        }

        public void UpdateStock(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("--Criar Throw Exception---");

            QuantityStock = newQuantity;
        }
    }
}
   
