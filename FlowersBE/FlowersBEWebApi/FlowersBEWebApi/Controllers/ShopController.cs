
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
                return _shopService.GetShops();
            } catch (Exception e)
            {
                _logger.LogError($"{nameof(GetAll)}, {e}");
                return StatusCode(500);
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<ShopModel>> Get(long id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Get)}, {id}");
                var shop = _shopService.GetShop(id);

                if(shop == null)
                {
                    return StatusCode(404);
                }

                return shop;
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
                _shopService.Add(shop);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Add)}, {shop}, {e}");
                return StatusCode(500);
            }
        }

        [HttpPost("remove")]
        public async Task<ActionResult> Remove(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Remove)}, {shop}");
                _shopService.Remove(shop);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Remove)}, {shop}, {e}");
                return StatusCode(500);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Update)}, {shop}");
                _shopService.Update(shop);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)}, {shop}, {e}");
                return StatusCode(500);
            }
        }
    }
}

