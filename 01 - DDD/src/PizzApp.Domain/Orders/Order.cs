using System.Collections.Generic;
using System.Linq;

using PizzApp.Domain.Products;

namespace PizzApp.Domain.Orders
{
    public class Order
    {
        private readonly HashSet<OrderLine> _items = new HashSet<OrderLine>();

        public IEnumerable<OrderLine> Items => this._items;

        public ProductAddedToOrder AddToOrder(Product product)
        {
            var p = this._items.FirstOrDefault(x => x.Product == product);
            return null;
        }

    }
}
