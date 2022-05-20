using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Orders
{
    public interface IOrdersService
    {
        public BaseResult CreateOrder(OrderModel model);
        public BaseResult UpdateOrder(OrderModel model);
        public BaseResult DeleteOrder(int id);
    }
}
