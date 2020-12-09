using System.Collections.Generic;

namespace FoodToGo.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}