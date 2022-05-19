using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Shops;

namespace FlowersBEWebApi.Services.Shops
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<ShopService> _logger;
        private readonly DataContext _dataContext;

        public ShopService(IShopRepository shopRepository, ILogger<ShopService> logger, DataContext dataContext)

        {
            _shopRepository = shopRepository;
            _logger = logger;
            _dataContext = dataContext;
        }

        public BaseResult Add(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Add)} ({shop})");
                _shopRepository.Add(ConvertToShop(shop));
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Add)}, {shop}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetShop(long id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetShop)}, {id}");
                var shop = _shopRepository.Get(id);
                if (shop is null)
                {
                    _logger.LogWarning($"[{nameof(ShopService)}] {nameof(GetShop)} ({id}): Shop with the given ID was not found");
                    return new BaseResult(false, 404, "Shop with the given ID not found");
                }

                return new BaseResult(true, 200, "", ConvertToModel(shop));
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShop)}, {id}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }

        }

        public BaseResult GetShops()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetShops)}");

                var shopList = new List<ShopModel>();

                _shopRepository.GetAll().ForEach(shop => shopList.Add(ConvertToModel(shop)));
                if (shopList is null || shopList.Count == 0)
                {
                    _logger.LogWarning($"[{nameof(ShopService)}] {nameof(GetShops)}: No shops were found");
                    return new BaseResult(false, 404, "No shops were found");
                }
                return new BaseResult(true, 200, "", shopList);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShops)}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }
            
        }

        public BaseResult Remove(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Remove)}, {id}");

                var shop = _shopRepository.Get(id);
                if (shop is null)
                {
                    _logger.LogWarning($"[{nameof(ShopService)}] {nameof(Remove)}: Shop with the following ID: ({id}) was not found");
                    return new BaseResult(false, 404, "Shop with the given ID not found");
                }
                _shopRepository.Remove(id);
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Remove)}, {id}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }

        }

        public BaseResult Update(ShopModel shop, int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Update)}, {shop}");

                var checkRes = GetShop(id);
                if (!checkRes.Success)
                    return checkRes;

                _shopRepository.Update(ConvertToShop(shop));
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)}, {shop}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetNewlyCreated(DateTime date)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetNewlyCreated)} ({date})");
                var shopsRes = GetShops();
                if (!shopsRes.Success)
                    return shopsRes;
                else
                {
                    var newlyCreatedShops = ((List<ShopModel>)shopsRes.ReturnObject).Where(shop => shop.CreatedAt >= date).ToList();
                    return new BaseResult(true, 200, "", newlyCreatedShops);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Update)} ({date}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetTop(float rating)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTop)} ({rating})");

                var shopList = new List<ShopModel>();
                _shopRepository.GetTop(rating).ForEach(shop => shopList.Add(ConvertToModel(shop)));

                if (shopList is null || shopList.Count == 0)
                {
                    _logger.LogWarning($"[{nameof(ShopService)}] {nameof(GetTop)} ({rating}): No shops with such or better rating were found");
                    return new BaseResult(false, 404, "No shops with such or better rating were found");
                }                    

                return new BaseResult(true, 200, "", shopList);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShops)}, {e}");
                return new BaseResult(false, 500, "Internal server error");
            }

        }

        private ShopModel ConvertToModel(Shop shop)
        {
            decimal deliveryPrice = 0;
            float deliveryDistance = 0;

            return new ShopModel
            {
                Id = shop.Id,
                Name = shop.Name,
                Phone = shop.Phone,
                ImagePath = shop.ImagePath,
                Description = shop.Description,
                Address = shop.Address,
                City = shop.City,
                OpeningHour = shop.OpeningHour,
                ClosingHour = shop.ClosingHour,
                DeliveryPrice = deliveryPrice,
                DeliveryDistance = deliveryDistance,
                Rating = shop.Rating,
                CreatedAt = shop.CreatedAt,
                UpdatedAt = shop.UpdatedAt,
            };
        }

        private Shop ConvertToShop(ShopModel shopModel)
        {
            return new Shop
            {
                Name = shopModel.Name,
                Phone = shopModel.Phone,
                ImagePath = shopModel.ImagePath,
                Description = shopModel.Description,
                Address = shopModel.Address,
                City = shopModel.City,
                OpeningHour = shopModel.OpeningHour,
                ClosingHour = shopModel.ClosingHour,
                Rating = shopModel.Rating,
                CreatedAt = shopModel.CreatedAt,
                UpdatedAt = shopModel.UpdatedAt,
            };
        }
    }
}