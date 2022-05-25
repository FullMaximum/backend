using FlowersBEWebApi.Helpers;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers.Orders
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

        //[Authorize]
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
                _logger.LogError($"[{nameof(OrdersController)}] {nameof(Create)} ({model.ToString()}): {ex}");
                return StatusCode(500);
            }
        }

        [HttpGet("byOrderId/{id}")]
        public async Task<ActionResult<BaseResult>> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrdersController)}] {nameof(GetById)} (Id: {id})");
                var res = _service.GetById(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersController)}] {nameof(GetById)} (Id: {id}): {ex}");
                return StatusCode(500);
            }
        }

        [HttpGet("byUserId/{userId}")]
        public async Task<ActionResult<BaseResult>> GetByUserId(int userId)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrdersController)}] {nameof(GetByUserId)} (Id: {userId})");
                var res = _service.GetByUserId(userId);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersController)}] {nameof(GetByUserId)} (Id: {userId}): {ex}");
                return StatusCode(500);
            }
        }
    }
}
