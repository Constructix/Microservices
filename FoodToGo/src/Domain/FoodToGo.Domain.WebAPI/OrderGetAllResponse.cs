using System;
using System.Collections.Generic;

namespace FoodToGo.Domain.WebAPI
{
    public class OrderGetAllResponse
    {
        public DateTime Created { get; set; }
        public string Message { get; set; }
        public List<Order> Orders { get; set; }
    }
}