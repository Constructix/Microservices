using System.Collections.Generic;
using System.IO;
using System.Linq;
using FoodToGo.Domain;
using Newtonsoft.Json;

namespace FoodToGo.Repositories
{
    public class JsonFileOrderRepository : IOrderRepository
    {
        private List<Order> _orders;
        private string FileName { get; set; }

        public JsonFileOrderRepository(string jsonFile)
        {
            ReadOrdersFromStore(jsonFile);
            this.FileName = jsonFile;
        }
        private void ReadOrdersFromStore(string DataFile)
        {
            if (!File.Exists(DataFile))
            {
                _orders ??= new List<Order>();

                var orderIds = Enumerable.Range(1, 10);
                for (var i = 0; i < orderIds.Count(); i++) _orders.Add(OrderFactoryTestCreator.Generate(i));

                SaveOrders(DataFile);
            }
            else
            {
                _orders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(DataFile));
            }
        }
        private void SaveOrders(string DataFile)
        {
            var json = JsonConvert.SerializeObject(_orders);
            File.WriteAllText(DataFile, json);
        }

        public List<Order> GetAll() => _orders;

        public Order Get(int id) => _orders.Find(x => x.Id.Equals(id)) ?? null;
        public void Save(Order newOrder)
        {
            newOrder.Id = (_orders.Max(x => x.Id) + 1);
            SaveOrders(this.FileName);
        }
    }
}