using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Shops
{
    public interface IShopService
    {
        public BaseResult GetShops();
        public BaseResult GetTop(float rating);
        public BaseResult GetShop(long id);
        public BaseResult Add(ShopModel shop);
        public BaseResult Remove(int id);
        public BaseResult Update(ShopModel shop, int id);
        public BaseResult GetNewlyCreated(DateTime date);

    }
}

