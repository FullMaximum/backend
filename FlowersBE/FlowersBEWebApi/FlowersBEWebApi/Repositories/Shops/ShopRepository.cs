using System;
using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Shops
{
	public class ShopRepository: IShopRepository
	{
		private readonly DataContext _context;

		public ShopRepository(DataContext context)
        {
			_context = context;
        }

        public void Add(Shop shop)
        {
            _context.Shops.Add(shop);
        }

        public List<Shop> GetTop(float rating)
        {
            return _context.Shops.Where(shop => shop.Rating >= rating).ToList();
        }

        public Shop Get(long id)
        {
            return _context.Shops.FirstOrDefault(shop => shop.Id == id);
        }

        public List<Shop> GetAll()
        {
            return _context.Shops.ToList();
        }

        public void Remove(int id)
        {
            var shop = Get(id);
            _context.Shops.Remove(shop);
        }

        public void Update(Shop shop)
        {
            _context.Shops.Update(shop);
        }
    }
}

