using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Flowers;
using Microsoft.Extensions.Caching.Memory;

namespace FlowersBEWebApi.Services.Flowers
{
    public class FlowersServiceCachingDecorator : IFlowersService
    {
        private readonly IFlowersService _flowersService;
        private readonly IMemoryCache _memoryCache;
        private readonly IFlowerRepository _repository;
        private readonly ILogger<FlowersServiceCachingDecorator> _logger;
        private readonly DataContext _context;
        private const string ALL_FLOWERS_CACHE_KEY = "flowers.all";

        public FlowersServiceCachingDecorator(IFlowersService flowersService, IMemoryCache cache, IFlowerRepository repository,
            ILogger<FlowersServiceCachingDecorator> logger,
            DataContext dataContext)
        {
            _flowersService = flowersService;
            _memoryCache = cache;
            _repository = repository;
            _logger = logger;
            _context = dataContext;
        }

        public BaseResult DeleteFlowerById(int id)
        {
            _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)} ({id})");
            try
            {
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)}: Flower with the following ID: ({id}) was not found");
                    return new BaseResult(false, 404, "Flower with the given ID not found");
                }
                _repository.Delete(id);
                _context.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)} ({id}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetAll()
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetAll)}");
                List<FlowerModel> flowersModelled = null;

                if (!_memoryCache.TryGetValue(ALL_FLOWERS_CACHE_KEY, out flowersModelled))
                {
                    var flowers = _repository.GetAll();
                    if (flowers.Count == 0 || flowers is null)
                    {
                        _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetAll)}: No flowers were found");
                        return new BaseResult(false, 404, "No flowers were found");
                    }
                    flowersModelled = flowers.Select(x => new FlowerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ShopId = x.ShopId,
                        Category = x.Category,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                    }).ToList();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _memoryCache.Set(ALL_FLOWERS_CACHE_KEY, flowersModelled, cacheEntryOptions);
                }
                return new BaseResult(true, 200, "", flowersModelled);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetAll)}: ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetByCategory(string category)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetByCategory)} ({category})");
                var flowers = _repository.GetByCategory(category);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetByCategory)}: no flowers were found for category: ({category})");
                    return new BaseResult(false, 404, "No flowers found for the given category");
                }
                var catFlower = flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    ShopId = x.ShopId,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
                return new BaseResult(true, 200, "", catFlower);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetByCategory)} (Category: {category}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetById(int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetById)} (Id: {id})");
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetById)}: no flowers were found with ID: ({id})");
                    return new BaseResult(false, 404, "Flower with the given ID not found");
                }
                var modelledFlower = new FlowerModel
                {
                    Id = id,
                    Name = flower.Name,
                    Category = flower.Category,
                    ShopId = flower.ShopId,
                    Price = flower.Price,
                    ImagePath = flower.ImagePath
                };
                return new BaseResult(true, 200, "", modelledFlower);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetById)} (Id: {id}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetByPrice(decimal price)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetByPrice)} (Price: {price})");
                var flowers = _repository.GetByPrice(price);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetByPrice)}: no flowers were found with price: ({price})");
                    return new BaseResult(false, 404, "No flowers were found with the given price");
                }
                var pricedFlowers = flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    ShopId = x.ShopId,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
                return new BaseResult(true, 200, "", pricedFlowers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetByPrice)} (Price: {price}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult SaveNewFlower(FlowerModel flower)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(SaveNewFlower)} ({flower.ToString()})");
                _repository.Add(new Flower
                {
                    Name = flower.Name,
                    Price = flower.Price,
                    Category = flower.Category,
                    ShopId = flower.ShopId,
                    ImagePath = flower.ImagePath,
                });
                _context.SaveChanges();
                return new BaseResult(true, 201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(SaveNewFlower)} ({flower.ToString()}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult UpdateFlowerData(FlowerModel flower, int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()})");

                var checkRes = GetById(id);
                if (!checkRes.Success)
                    return checkRes;

                _repository.Update(new Flower
                {
                    Id = id,
                    Name = flower.Name,
                    Price = flower.Price,
                    ShopId = flower.ShopId,
                    Category = flower.Category,
                    ImagePath = flower.ImagePath,
                });
                _context.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }
        public BaseResult GetByShopId(long shopId)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetByShopId)} (ShopId: {shopId})");

                var cacheKey = $"flowers_shop_{shopId}";
                List<FlowerModel> shopFlowers = null;

                if (!_memoryCache.TryGetValue(cacheKey, out shopFlowers))
                {
                    var flowers = _repository.GetByShopId(shopId);
                    if (flowers.Count == 0 || flowers is null)
                    {
                        _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetByShopId)}: no flowers were found for shop: ({shopId})");
                        return new BaseResult(false, 404, "No flowers found for the given shop");
                    }
                    shopFlowers = flowers.Select(x => new FlowerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category,
                        ShopId = x.ShopId,
                        Price = x.Price,
                        ImagePath = x.ImagePath,
                    }).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _memoryCache.Set(cacheKey, shopFlowers, cacheEntryOptions);
                }
                
                return new BaseResult(true, 200, "", shopFlowers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetByCategory)} (ShopId: {shopId}): {ex}");
                return new BaseResult(false, 500, "Internal server error");
            }
        }
    }
}
