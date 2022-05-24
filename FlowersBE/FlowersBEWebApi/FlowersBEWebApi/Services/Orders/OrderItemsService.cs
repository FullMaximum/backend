using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Mappers.Orders;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Orders;

namespace FlowersBEWebApi.Services.Orders
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly DataContext _dataContext;
        private readonly ILogger<OrderItemsService> _logger;
        private readonly IOrderItemMapper _mapper;

        public OrderItemsService(IOrderItemsRepository orderItemsRepository, DataContext dataContext, ILogger<OrderItemsService> logger, IOrderItemMapper mapper)
        {
            _orderItemsRepository = orderItemsRepository;
            _dataContext = dataContext;
            _logger = logger;
            _mapper = mapper;
        }

        public BaseResult DeleteBulkByOrderId(int orderId)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(DeleteBulkByOrderId)} (OrderId: {orderId})");
            try
            {
                _orderItemsRepository.BulkDeleteByOrderId(orderId);
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(DeleteBulkByOrderId)} (OrderId: {orderId}): {ex}");
                return new BaseResult(false, 422, "Lock exception occured");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(DeleteBulkByOrderId)} (OrderId: {orderId}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult DeleteOne(int id)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(DeleteOne)} (Id: {id})");
            try
            {
                _orderItemsRepository.Delete(id);
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(DeleteOne)} (Id: {id}): {ex}");
                return new BaseResult(false, 422, "Lock exception occured");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(DeleteOne)} (Id: {id}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult InsertBulk(List<OrderItemModel> models)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(InsertBulk)} (Models: {string.Join(';', models)})");
            try
            {
                var orderItems = new List<OrderItem>();
                foreach (var model in models)
                {
                    orderItems.Add(_mapper.Map(model));
                }
                _orderItemsRepository.BulkInsert(orderItems);
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(InsertBulk)} (Models: {string.Join(';', models)}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult InsertOne(OrderItemModel model)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(InsertOne)} (Model: {model.ToString()})");
            try
            {
                _orderItemsRepository.Insert(_mapper.Map(model));
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(InsertOne)} (Model: {model.ToString()}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult UpdateOne(OrderItemModel model, int id)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(UpdateOne)} (Id: {id},Model: {model.ToString()})");
            try
            {
                model.Id = id;
                _orderItemsRepository.Update(_mapper.Map(model));
                _dataContext.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(UpdateOne)} (Id: {id},Model: {model.ToString()}): {ex}");
                return new BaseResult(false, 422, "Lock exception occured");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(UpdateOne)} (Id: {id}, Model: {model.ToString()}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }

        public BaseResult GetByOrderId(int id)
        {
            _logger.LogInformation($"[{nameof(OrderItemsService)}] {nameof(GetByOrderId)} (OrderId: {id})");
            try
            {
                var items = _orderItemsRepository.GetByOrderId(id);
                if (items == null || items.Count < 1)
                {
                    _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(GetByOrderId)} (OrderId: {id}): No items were found for the given order id");
                    return new BaseResult(false, 404, "No items were found for the given order id");
                }
                return new BaseResult(true, 200, "", items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(OrderItemsService)}] {nameof(InsertOne)} (OrderId: {id}): {ex}");
                return new BaseResult(false, 500, "Internal Server error");
            }
        }
    }
}
