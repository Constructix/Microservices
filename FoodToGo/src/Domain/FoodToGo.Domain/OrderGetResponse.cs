using System;
using System.Collections.Generic;

namespace FoodToGo.Domain
{
    public class OrderGetAllResponse
    {
        public string Message { get; set; }

        public DateTime Created { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class OrderGetResponse
    {
        public OrderGetResponse()
        {
            Created = DateTime.Now;
        }

        public string Message { get; set; }

        public DateTime Created { get; set; }

        public Order Order { get; set; }
    }


    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Product
    {
        public string SkuId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int UnitPrice { get; set; }
    }
}