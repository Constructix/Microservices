using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FoodToGo.Domain;
using FoodToGo.Repositories;
using Newtonsoft.Json;

namespace FoodToGo.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;
        
        public Order RetrieveOrder(int id) =>  _orderRepository.Get(id);
        public List<Order> RetrieveAllOrders() => _orderRepository.GetAll();

        public int AddOrder(Order newOrder)
        {
            _orderRepository.Save(newOrder);
            return newOrder.Id;
        }
    }
}