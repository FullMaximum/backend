
using FlowersBEWebApi.Helpers;
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

        [Authorize]
        [HttpGet("getAll")]
        public async Task<ActionResult<List<ShopModel>>> GetAll()
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(GetAll)}");
                var res =  _shopService.GetShops();
                return StatusCode(res.StatusCode, res);
            } catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(GetAll)}, {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("getTop/{rating}")]
        public async Task<ActionResult<List<ShopModel>>> GetTop(float rating)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(GetTop)} (Rating: {rating})");
                var res = _shopService.GetTop(rating);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(GetTop)} (Rating: {rating}): {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ShopModel>> Get(long id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(Get)} (Id: {id})");
                var res = _shopService.GetShop(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(Get)} (Id: {id}): {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult> Add(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(Add)} ({shop})");
                var res = _shopService.Add(shop);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(Add)} ({shop}): {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("remove/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(Remove)} (Id: {id})");
                var res = _shopService.Remove(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(Remove)} (Id: {id}): {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<ActionResult> Update(ShopModel shop, int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(Update)} (Id: {id}, {shop})");
                var res = _shopService.Update(shop, id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(Update)} ({shop}): {e}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("getNew/{date}")]
        public async Task<ActionResult<List<ShopModel>>> GetNew(DateTime date)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ShopController)}] {nameof(GetNew)} ({date})");
                var res = _shopService.GetNewlyCreated(date);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception e)
            {
                _logger.LogError($"[{nameof(ShopController)}] {nameof(GetNew)} ({date}): {e}");
                return StatusCode(500);
            }
        }
    }
}

