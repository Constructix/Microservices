using System;

namespace FoodToGo.Domain.WebAPI
{
    public class OrderAddedResponse
    {
        public DateTime Created { get; set; }
        public string Message { get; set; }
    }
}