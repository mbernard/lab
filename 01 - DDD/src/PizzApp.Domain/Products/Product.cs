namespace PizzApp.Domain.Products
{
    public class Product
    {
        public Product(ProductName productName, Price price)
        {
            this.ProductName = productName;
            this.Price = price;
        }

        public ProductName ProductName { get; }

        public Price Price { get; }
    }
}