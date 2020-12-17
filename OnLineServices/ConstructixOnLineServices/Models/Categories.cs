using System;
using System.Collections.Generic;

namespace ConstructixOnLineServices.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Category> SubCategories { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }


        public Category()
        {
            Id = Guid.NewGuid().ToString();
            SubCategories = new List<Category>();
        }
    }
}