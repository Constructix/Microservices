using System;
using FoodToGo.Domain;
using FoodToGo.Domain.WebAPI;
using FoodToGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodToGo.WebAPI.Controllers
{
    [ApiController]
    public class OrderServiceController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderServiceController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        ///     Return Order that matches OrderId
        /// </summary>
        /// <param name="OrderId">Unique Order Id to retrieve details about current order.</param>
        /// <returns>OrderGetResponse containing message about the order.</returns>
        [HttpGet]
        [Route("api/[controller]/{orderId}")]
        public ActionResult<OrderGetResponse> Get(int orderId)
        {
            var response = _orderService.RetrieveOrder(orderId);
            if (response != null)
                return new OkObjectResult(new OrderGetResponse {Message = "OK", Order = response});
            return new NotFoundObjectResult(new OrderGetResponse {Message = "Does not exist"});
        }

        [HttpGet]
        [Route("api/[controller]")]
        public ActionResult<OrderGetAllResponse> Get()
        {
            var response = _orderService.RetrieveAllOrders();


            return new OkObjectResult(new OrderGetAllResponse
                {Orders = response, Created = DateTime.Now, Message = "OK"});
        }

        [HttpPost]
        [Route("api/[controller]/AddOrder")]
        public ActionResult<OrderAddedResponse> OrderAdd([FromBody] Order newOrder)
        {
            var response = _orderService.AddOrder(newOrder);
            return new OkObjectResult(new OrderAddedResponse
            {
                Message = "Ok", Created =DateTime.Now
            });
        }
        
    }
}