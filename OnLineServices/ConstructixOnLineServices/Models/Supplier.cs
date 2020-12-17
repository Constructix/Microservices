using System;
using System.Collections.Generic;

namespace ConstructixOnLineServices.Models
{
    public class Supplier   
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string webAddress { get; set; }
        public string email { get; set; }
        public List<Category> Categories { get; set; }

        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }


    }
}