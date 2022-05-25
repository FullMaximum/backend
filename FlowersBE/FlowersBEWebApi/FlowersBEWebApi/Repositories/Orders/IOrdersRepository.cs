using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Orders
{
    public interface IOrdersRepository
    {
        public void Insert(Order order);
        public void Update(Order order);
        public void Delete(int id);
        public List<Order> GetAll();
        public Order GetById(int id);
        public List<Order> GetByUserId(int userId);
        public List<Order> GetByShopId(int shopId);
    }
}
