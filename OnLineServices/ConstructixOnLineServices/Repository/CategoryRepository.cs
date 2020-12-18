using System;
using System.Collections.Generic;
using ConstructixOnLineServices.Models;

namespace ConstructixOnLineServices.Repository
{
    public class CategoryRepository : IRepository<Category, string>
    {
        private List<Category> _items;
        public Category Get(string id) => _items.Find(x => x.Name.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        public Category GetById(Guid id) => _items.Find(x => x.Id.Equals(id));

        public List<Category> GetAll() => _items;

        public CategoryRepository(List<Category> items) => _items = items;
    }
}