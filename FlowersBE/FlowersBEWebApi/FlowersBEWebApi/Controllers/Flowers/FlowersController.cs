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
                var flower = _flowersService.GetById(id);
                if (flower is null)
                    return NotFound();
                else
                    return Ok(flower);
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
                var flowers = _flowersService.GetAll();
                if (flowers.Count == 0)
                {
                    _logger.LogWarning($"{nameof(GetAll)} No flowers were found");
                    return NoContent();
                }
                return Ok(flowers);
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
                var flowers = _flowersService.GetByCategory(category);
                if (flowers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(flowers);
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
                var flowers = _flowersService.GetByPrice(price);
                if (flowers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(flowers);
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
                if (_flowersService.SaveNewFlower(model))
                    return StatusCode(201);
                else
                    return BadRequest();
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
                if (_flowersService.UpdateFlowerData(model, id))
                    return Ok();
                else
                    return BadRequest();
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
                if (_flowersService.DeleteFlowerById(id))
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Delete)} ({id}): ({ex})");
                return StatusCode(500);
            }
        }
    }
}
