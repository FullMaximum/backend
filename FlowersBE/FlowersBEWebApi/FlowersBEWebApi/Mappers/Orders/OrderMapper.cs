using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Mappers.Orders
{
    public class OrderMapper : IOrderMapper
    {
        public Order Map(OrderModel orderModel)
        {
            if (orderModel == null)
                return null;
            return new Order
            {
                Id = orderModel.Id,
                UserId = orderModel.UserId,
                ShopId = orderModel.ShopId,
                Status = orderModel.Status,
                OrderTotal = orderModel.OrderTotal,
                DeliveryAddress = orderModel.DeliveryAddress,
                CreatedAt = orderModel.CreatedAt ?? DateTime.UtcNow,
                UpdatedAt = orderModel.UpdatedAt ?? DateTime.UtcNow,
                RowVersion = orderModel.RowVersion,
            };
        }

        public OrderModel Map(Order order)
        {
            if (order == null)
                return null;
            return new OrderModel
            {
                Id = order.Id,
                UserId = order.UserId,
                ShopId = order.ShopId,
                Status = order.Status,
                OrderTotal = order.OrderTotal,
                DeliveryAddress = order.DeliveryAddress,
                CreatedAt = order.CreatedAt,
                UpdatedAt= order.UpdatedAt,
                RowVersion= order.RowVersion,
            };
        }
    }
}
