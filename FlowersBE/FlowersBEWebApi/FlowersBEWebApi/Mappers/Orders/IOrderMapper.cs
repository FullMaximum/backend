using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Mappers.Orders
{
    public interface IOrderMapper
    {
        public Order Map(OrderModel orderModel);
        public OrderModel Map(Order order);
    }
}
