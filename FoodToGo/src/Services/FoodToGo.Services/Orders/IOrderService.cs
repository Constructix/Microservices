using System.Collections.Generic;
using FoodToGo.Domain;

namespace FoodToGo.Services
{
    public interface IOrderService
    {
        public Order RetrieveOrder(int id);

        public List<Order> RetrieveAllOrders();

        public int AddOrder(Order newOrder);
    }
}