using PizzApp.Domain.Products;

namespace PizzApp.Domain.Orders
{
    public class OrderLine
    {
        public OrderLine(Quantity quantity, Product product)
        {
            this.Quantity = quantity;
            this.Product = product;
        }

        public Quantity Quantity { get; }

        public Product Product { get; }
    }
}