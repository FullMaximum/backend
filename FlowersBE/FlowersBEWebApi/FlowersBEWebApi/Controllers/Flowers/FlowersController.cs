using FlowersBEWebApi.Models;
using FlowersBEWebApi.Services.Flowers;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers.Flowers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly ILogger<FlowersController> _logger;
        private readonly IFlowersService _flowersService;

        public FlowersController(ILogger<FlowersController> logger, IFlowersService flowersService)
        {
            _logger = logger;
            _flowersService = flowersService;
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<FlowerModel>> Get(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Get)} (Id: {id})");
                var res = _flowersService.GetById(id);
                return StatusCode(res.StatusCode, res);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(Get)} ({id}): ({ex})");
                return StatusCode(500);
            }
        }

        [HttpGet("getAllFlowers")]
        public async Task<ActionResult<List<FlowerModel>>> GetAll()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAll)}");
                var res = _flowersService.GetAll();
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetAll)} ({ex})");
                return StatusCode(500);
            }
        }

        [HttpGet("getByCategory/{category}")]
        public async Task<ActionResult<List<FlowerModel>>> GetByCategory(string category)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByCategory)} ({category})");
                var res = _flowersService.GetByCategory(category);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetByCategory)} ({category}): ({ex})");
                return StatusCode(500);
            }
        }

        [HttpGet("getByPrice/{price}")]
        public async Task<ActionResult<List<FlowerModel>>> GetByPrice(decimal price)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByPrice)} ({price})");
                var res = _flowersService.GetByPrice(price);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetByPrice)} ({price}): ({ex})");
                return StatusCode(500);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(FlowerModel model)
        {
            try
            {
                _logger.LogInformation($"{nameof(Add)} ({model.ToString()})");
                var res = _flowersService.SaveNewFlower(model);
                return StatusCode(res.StatusCode, res);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(Add)} ({model.ToString()}): ({ex})");
                return StatusCode(500);
            }
        }

        [HttpPost("update/{id}")]
        public async Task<ActionResult> Update(FlowerModel model, int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Update)} (Id: {id}, {model.ToString()})");
                var res = _flowersService.UpdateFlowerData(model, id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Update)} (Id: {id}, {model.ToString()}): ({ex})");
                return StatusCode(500);
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Delete)} (Id: {id})");
                var res = _flowersService.DeleteFlowerById(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Delete)} ({id}): ({ex})");
                return StatusCode(500);
            }
        }
    }
}
