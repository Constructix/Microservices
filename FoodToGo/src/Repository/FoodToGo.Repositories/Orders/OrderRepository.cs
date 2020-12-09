using System;
using System.Collections.Generic;
using FoodToGo.Domain;

namespace FoodToGo.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> _orders;

        public OrderRepository() => _orders  = new List<Order>();
        
        public List<Order> GetAll() => _orders;

        public Order Get(int id) => _orders.Find(x => x.Id.Equals(id)) ?? null;
        public void Save(Order newOrder)
        {
            throw new NotImplementedException();
        }

        public OrderRepository(List<Order> orders)
        {
            _orders = orders;
        }
        
    }
    
    
    public class OrderFactoryTestCreator
    {
        public static Order Generate(int id)
        {
            var items = new List<OrderItem>();
            var order = new Order {Id = id};
            order.OrderItems = items;

            var ran = new Random((int) DateTime.Now.Ticks);
            var productsGenerated = ProductFactoryTestCreator.Generate();

            order.OrderItems.Add(new OrderItem
                {Product = productsGenerated[ran.Next(0, productsGenerated.Count - 1)], Quantity = 1});

            return order;
        }
    }

    public class ProductFactoryTestCreator
    {
        public static List<Product> Generate()
        {
            var productNames = new[] {"First", "Second", "Third"};


            var productLength = new Random((int) DateTime.Now.Ticks);

            var products = new List<Product>();
            var productsToCreate = productLength.Next(1, 3);


            for (var i = 0; i < productsToCreate; i++)
            {
                var productNamesIndex = new Random((int) DateTime.Now.Ticks).Next(0, productNames.Length - 1);

                products.Add(new Product
                {
                    SkuId = $"00{i}", Name = productNames[productNamesIndex], Details = string.Empty, UnitPrice = 1000
                });
            }

            return products;
        }
    }
}