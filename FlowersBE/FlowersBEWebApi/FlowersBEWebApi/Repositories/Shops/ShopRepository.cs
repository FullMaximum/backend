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
            _context.SaveChanges();
        }

        public Shop Get(long id)
        {
            return _context.Shops.FirstOrDefault(shop => shop.Id == id);
        }

        public List<Shop> GetAll()
        {
            return _context.Shops.ToList();
        }

        public void Remove(Shop shop)
        {
            _context.Shops.Remove(shop);
            _context.SaveChanges();
        }

        public void Update(Shop shop)
        {
            _context.Shops.Update(shop);
            _context.SaveChanges();
        }
    }
}

