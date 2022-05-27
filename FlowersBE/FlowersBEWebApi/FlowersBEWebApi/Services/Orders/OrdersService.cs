using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Mappers.Orders;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Orders;
using FlowersBEWebApi.Enums;

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

        public BaseResult GetByUserId(int userId)
        {
            _logger.LogInformation($"[{nameof(OrdersService)}] {nameof(GetByUserId)} (UserId: {userId})");
            try
            {
                var orders = _ordersRepository.GetByUserId(userId);
                if (orders is null || orders.Count < 1)
                {
                    _logger.LogWarning($"[{nameof(OrdersService)}] {nameof(GetByUserId)} (UserId: {userId}): Orders with the given UserId not found");
                    return new BaseResult(false, 404, "Orders with the given UserId not found");
                }

                var returnList = new List<OrderModel>();
                foreach (var item in orders)
                {
                    var orderModel = _mapper.Map(item);
                    var orderItemsRes = _orderItemsService.GetByOrderId(item.Id);
                    if (!orderItemsRes.Success)
                    {
                        continue;
                    }
                    orderModel.Items = new List<Item>();
                    foreach (var orderItem in orderItemsRes.ReturnObject as List<OrderItem>)
                    {
                        orderModel.Items.Add(new Item { FlowerId = orderItem.FlowerId, Count = orderItem.Count });
                    }
                    returnList.Add(orderModel);
                }

                return new BaseResult(true, 200, "", returnList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersService)}] {nameof(GetByUserId)} (UserId: {userId}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResult UpdateOrder(OrderModel model)
        {
            _logger.LogInformation($"[{nameof(OrdersService)}] {nameof(UpdateOrder)} (Order: {model.ToString()})");
            try
            {
                if (!_ordersRepository.CheckIfExists(model.Id))
                {
                    _logger.LogError($"[{nameof(OrdersService)}] {nameof(UpdateOrder)} (Order: {model.ToString()}): Flower with such Id not found");
                    return new BaseResult(false, 404, "Flower with given Id not found");
                }

                _ordersRepository.Update(_mapper.Map(model));
                _context.SaveChanges();

                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersService)}] {nameof(UpdateOrder)} (Order: {model.ToString()}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult SimulateOrder(int orderId)
        {
            Order order = _ordersRepository.GetById(orderId);

            int status = (int)order.Status;

            if (status >= 4)
            {
                return new BaseResult(false, 400, "Order status is at max value");
            }

            order.Status = (OrderStatus)status + 1;

            _ordersRepository.Update(order);
            _context.SaveChanges();

            return new BaseResult(true, 200);
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
