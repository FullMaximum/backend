using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Mappers.Orders
{
    public interface IOrderItemMapper
    {
        public OrderItem Map(OrderItemModel model);
        public OrderItemModel Map(OrderItem orderItem);
    }
}
