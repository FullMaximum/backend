using System;
using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Shops
{
	public interface IShopRepository
	{
		public List<Shop> GetAll();

		public Shop Get(long id);

		public void Add(Shop shop);

		public void Update(Shop shop);

		public void Remove(Shop shop);

	}
}

