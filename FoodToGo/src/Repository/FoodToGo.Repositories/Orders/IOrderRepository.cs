using System.Collections.Generic;
using FoodToGo.Domain;

namespace FoodToGo.Repositories
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
        public Order Get(int id);

        public void Save(Order newOrder);
    }
}
