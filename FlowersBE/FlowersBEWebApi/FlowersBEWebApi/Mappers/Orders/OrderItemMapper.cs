using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Mappers.Orders
{
    public class OrderItemMapper : IOrderItemMapper
    {
        public OrderItem Map(OrderItemModel model)
        {
            return new OrderItem
            {
                Id = model.Id,
                OrderId = model.OrderId,
                FlowerId = model.FlowerId,
                Count = model.Count,
                RowVersion = model.RowVersion,
            };
        }

        public OrderItemModel Map(OrderItem orderItem)
        {
            return new OrderItemModel
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                FlowerId = orderItem.FlowerId,
                Count = orderItem.Count,
                RowVersion = orderItem.RowVersion,
            };
        }
    }
}
