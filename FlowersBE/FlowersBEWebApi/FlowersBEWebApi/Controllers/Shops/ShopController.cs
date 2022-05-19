
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Services.Shops;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShopController : ControllerBase
	{
        private readonly IShopService _shopService;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IShopService shopService, ILogger<ShopController> logger)
        {
            _shopService = shopService;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<ShopModel>>> GetAll()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAll)}");
                var res =  _shopService.GetShops();
                return StatusCode(res.StatusCode, res);
            } catch (Exception e)
            {
                _logger.LogError($"{nameof(GetAll)}, {e}");
                return StatusCode(500);
            }
        }

        [HttpGet("getTop/{rating}")]
        public async Task<ActionResult<List<ShopModel>>> GetTop(float rating)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTop)}");
                var res = _shopService.GetTop(rating);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetTop)}, {e}");
                return StatusCode(500);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ShopModel>> Get(long id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Get)}, {id}");
                var res = _shopService.GetShop(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Get)}, {id}, {e}");
                return StatusCode(500);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Add)}, {shop}");
                var res = _shopService.Add(shop);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Add)}, {shop}, {e}");
                return StatusCode(500);
            }
        }

        [HttpPost("remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Remove)}, {id}");
                var res = _shopService.Remove(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Remove)}, {id}, {e}");
                return StatusCode(500);
            }
        }

        [HttpPost("update/{id}")]
        public async Task<ActionResult> Update(ShopModel shop, int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Update)} ({shop})");
                var res = _shopService.Update(shop, id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)}, {shop}, {e}");
                return StatusCode(500);
            }
        }

        [HttpGet("getNew/{date}")]
        public async Task<ActionResult<List<ShopModel>>> GetNew(DateTime date)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetNew)} ({date})");
                var res = _shopService.GetNewlyCreated(date);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetNew)} ({date}): ({e})");
                return StatusCode(500);
            }
        }
    }
}

