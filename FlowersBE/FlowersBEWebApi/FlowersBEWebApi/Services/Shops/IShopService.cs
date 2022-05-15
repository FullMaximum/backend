﻿using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Shops
{
    public interface IShopService
    {
        public List<ShopModel> GetShops();
        public ShopModel GetShop(long id);
        public void Add(ShopModel shop);
        public void Remove(ShopModel shop);
        public void Update(ShopModel shop);

    }
}

