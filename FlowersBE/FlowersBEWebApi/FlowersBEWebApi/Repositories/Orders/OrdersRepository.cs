using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _dataContext;

        public OrdersRepository(DataContext context)
        {
            _dataContext = context;
        }

        public void Insert(Order order)
        {
            _dataContext.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _dataContext.Update(order);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if(order != null)
                _dataContext.Remove(order);
        }

        public List<Order> GetAll()
        {
            return _dataContext.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _dataContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetByUserId(int userId)
        {
            return _dataContext.Orders.Where(order => order.UserId == userId).OrderByDescending(order => order.Id).Take(5).ToList();
        }

        public List<Order> GetByShopId(int shopId)
        {
            return _dataContext.Orders.Where(order => shopId == order.ShopId).ToList();
        }

        public bool CheckIfExists(int id)
        {
            return _dataContext.Orders.Any(x => x.Id == id);
        }
    }
}
