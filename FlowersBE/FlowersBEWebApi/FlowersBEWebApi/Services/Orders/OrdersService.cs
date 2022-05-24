using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Mappers.Orders;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Orders;

namespace FlowersBEWebApi.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly DataContext _context;
        private readonly ILogger<OrdersService> _logger;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsService _orderItemsService;
        private readonly IOrderMapper _mapper;

        public OrdersService(DataContext context, ILogger<OrdersService> logger, IOrdersRepository repo, IOrderItemsService orderItemsService, IOrderMapper mapper)
        {
            _context = context;
            _logger = logger;
            _ordersRepository = repo;
            _orderItemsService = orderItemsService;
            _mapper = mapper;
        }

        public BaseResult CreateOrder(OrderModel model)
        {
            _logger.LogInformation($"[{nameof(OrdersService)}] {nameof(CreateOrder)} ({model})");
            try
            {
                var order = _mapper.Map(model);
                _ordersRepository.Insert(order);
                _context.SaveChanges();
                return LinkOrderItems(model.Items, order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersService)}] {nameof(CreateOrder)} (Model: {model}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult GetById(int id)
        {
            _logger.LogInformation($"[{nameof(OrdersService)}] {nameof(GetById)} (Id: {id})");
            try
            {
                var order = _mapper.Map(_ordersRepository.GetById(id));
                if (order is null)
                {
                    _logger.LogWarning($"[{nameof(OrdersService)}] {nameof(GetById)} (Id: {id}): Order with the given Id not found");
                    return new BaseResult(false, 404, "Order with the given Id not found");
                }

                var orderItemsRes = _orderItemsService.GetByOrderId(id);
                if (!orderItemsRes.Success)
                {
                    return orderItemsRes;
                }
                order.Items = new List<Item>();
                foreach (var item in orderItemsRes.ReturnObject as List<OrderItem>)
                {
                    order.Items.Add(new Item { FlowerId = item.FlowerId, Count = item.Count });
                }

                return new BaseResult(true, 200, "", order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersService)}] {nameof(GetById)} (Id: {id}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResult UpdateOrder(OrderModel model)
        {
            throw new NotImplementedException();
        }

        private BaseResult LinkOrderItems(List<Item> items, int orderId)
        {
            var orderItems = new List<OrderItemModel>();
            foreach (var item in items)
            {
                orderItems.Add(new OrderItemModel
                {
                    OrderId = orderId,
                    FlowerId = item.FlowerId,
                    Count = item.Count,
                });
            }
            return _orderItemsService.InsertBulk(orderItems);
        }
    }
}
