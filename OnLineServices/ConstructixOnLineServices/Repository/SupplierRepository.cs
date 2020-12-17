using System;
using System.Collections.Generic;
using ConstructixOnLineServices.Models;

namespace ConstructixOnLineServices.Repository
{
    public class SupplierRepository : IRepository<Supplier, string>
    {
        private List<Supplier> _items;

        public Supplier Get(string id) => _items.Find(x => x.Name.Equals(id, StringComparison.CurrentCultureIgnoreCase));

        public List<Supplier> GetAll() =>  _items;

        public SupplierRepository(List<Supplier> items) => _items = items;
    }
}