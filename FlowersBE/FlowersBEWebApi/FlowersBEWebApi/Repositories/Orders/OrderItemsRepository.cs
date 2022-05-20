using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Orders
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly DataContext _context;

        public OrderItemsRepository(DataContext context)
        {
            _context = context;
        }

        public void Insert(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void BulkInsert(List<OrderItem> items)
        {
            _context.OrderItems.BulkInsert(items, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnInsertExpression = item => new { item.Id };
            });
        }

        public void Update(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public void Delete(int id)
        {
            var orderItem = GetById(id);
            if(orderItem != null)
                _context.OrderItems.Remove(orderItem);
        }

        public void BulkDeleteByOrderId(int orderId)
        {
            var orderItems = GetByOrderId(orderId);
            if (orderItems != null && orderItems.Count > 0)
            {
                _context.OrderItems.BulkDelete(orderItems);
            }
        }

        public OrderItem GetById(int id)
        {
            return _context.OrderItems.FirstOrDefault(x => x.Id == id);
        }

        public List<OrderItem> GetAll()
        {
            return _context.OrderItems.ToList();
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItems.Where(x => x.OrderId == orderId).ToList();
        }
    }
}
