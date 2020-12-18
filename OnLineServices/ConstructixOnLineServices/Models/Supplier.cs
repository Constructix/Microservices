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
        public List<Location> Locations { get; set; }

        public Supplier()
        {
            Id = Guid.NewGuid().ToString();
            Locations = new List<Location>();
        }


    }

    public class Location
    {
        public Address Address { get; set; }
        public string Phone { get; set; }
    }

    public class Address
    {
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }

    }
}