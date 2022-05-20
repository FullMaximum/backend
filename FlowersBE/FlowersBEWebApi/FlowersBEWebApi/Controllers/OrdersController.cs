using FlowersBEWebApi.Models;
using FlowersBEWebApi.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _service;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("/add")]
        public async Task<ActionResult<BaseResult>> Create(OrderModel model)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrdersController)}] {nameof(Create)} ({model.ToString()})");
                var res = _service.CreateOrder(model);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersController)}] {nameof(Create)} ({model.ToString()}): ({ex})");
                return StatusCode(500);
            }
        }
    }
}
