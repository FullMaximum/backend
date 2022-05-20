using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Orders
{
    public interface IOrderItemsRepository
    {
        public void Insert(OrderItem orderItem);
        public void BulkInsert(List<OrderItem> orderItems);
        public void Update(OrderItem orderItem);
        public void Delete(int id);
        public void BulkDeleteByOrderId(int orderId);
        public List<OrderItem> GetAll();
        public OrderItem GetById(int id);
        public List<OrderItem> GetByOrderId(int orderId);
    }
}
