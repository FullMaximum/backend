using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Shops;

namespace FlowersBEWebApi.Services.Shops
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<ShopService> _logger;

        public ShopService(IShopRepository shopRepository, ILogger<ShopService> logger)

        {
            _shopRepository = shopRepository;
            _logger = logger;
        }

        public void Add(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Add)}, {shop}");
                _shopRepository.Add(ConvertToShop(shop));

            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Add)}, {shop}, {e}");
            }
        }

        public ShopModel GetShop(long id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetShop)}, {id}");
                var shop = _shopRepository.Get(id);

                return ConvertToModel(shop);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShop)}, {id}, {e}");

                return null;
            }

        }

        public List<ShopModel> GetShops()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetShops)}");

                var shopList = new List<ShopModel>();

                _shopRepository.GetAll().ForEach(shop => shopList.Add(ConvertToModel(shop)));

                return shopList;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShops)}, {e}");

                return null;
            }
            
        }

        public List<ShopModel> GetTop(float rating)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTop)}");

                var shopList = new List<ShopModel>();

                _shopRepository.GetTop(rating).ForEach(shop => shopList.Add(ConvertToModel(shop)));

                return shopList;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetShops)}, {e}");

                return null;
            }

        }

        public void Remove(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Remove)}, {shop}");
                _shopRepository.Remove(ConvertToShop(shop));

            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Remove)}, {shop}, {e}");
            }

        }

        public void Update(ShopModel shop)
        {
            try
            {
                _logger.LogInformation($"{nameof(Update)}, {shop}");
                _shopRepository.Update(ConvertToShop(shop));


            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)}, {shop}, {e}");
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
                Rating = shop.Rating
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
                Rating = shopModel.Rating

            };
        }
    }
}