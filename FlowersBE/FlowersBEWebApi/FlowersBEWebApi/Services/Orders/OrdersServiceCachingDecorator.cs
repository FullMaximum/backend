﻿using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Mappers.Orders;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Orders;
using Microsoft.Extensions.Caching.Memory;

namespace FlowersBEWebApi.Services.Orders
{
    public class OrdersServiceCachingDecorator : IOrdersService
    {
        private readonly IOrdersService _ordersService;
        private readonly DataContext _context;
        private readonly ILogger<OrdersService> _logger;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsService _orderItemsService;
        private readonly IOrderMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public OrdersServiceCachingDecorator(IOrdersService ordersService, DataContext context, ILogger<OrdersService> logger, 
            IOrdersRepository repo, 
            IOrderItemsService orderItemsService, 
            IOrderMapper mapper,
            IMemoryCache cache)
        {
            _ordersService = ordersService;
            _context = context;
            _logger = logger;
            _ordersRepository = repo;
            _orderItemsService = orderItemsService;
            _mapper = mapper;
            _memoryCache = cache;
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
                OrderModel order = null;
                var cacheKey = $"order_id_{id}";
                if (_memoryCache.TryGetValue(cacheKey, out order))
                {
                    order = _mapper.Map(_ordersRepository.GetById(id));
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
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _memoryCache.Set(cacheKey, order, cacheEntryOptions);
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
                var cacheKey = $"orders_userId_{userId}";
                List<OrderModel> returnList = null;
                if(!_memoryCache.TryGetValue(cacheKey, out returnList))
                {
                    var orders = _ordersRepository.GetByUserId(userId);
                    if (orders is null || orders.Count < 1)
                    {
                        _logger.LogWarning($"[{nameof(OrdersService)}] {nameof(GetByUserId)} (UserId: {userId}): Orders with the given UserId not found");
                        return new BaseResult(false, 404, "Orders with the given UserId not found");
                    }

                    returnList = new List<OrderModel>();
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
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _memoryCache.Set(cacheKey, returnList, cacheEntryOptions);
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

                var cacheKey = $"order_id_{model.Id}";
                OrderModel temp = null;
                if (_memoryCache.TryGetValue(cacheKey, out temp))
                {
                    _memoryCache.Remove(cacheKey);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _memoryCache.Set(cacheKey, model, cacheEntryOptions);
                }

                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrdersService)}] {nameof(GetByUserId)} (Order: {model.ToString()}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
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
