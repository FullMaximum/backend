using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Orders
{
    public interface IOrderItemsService
    {
        public BaseResult InsertOne(OrderItemModel model);
        public BaseResult InsertBulk(List<OrderItemModel> models);
        public BaseResult UpdateOne(OrderItemModel model, int id);
        public BaseResult DeleteOne(int id);
        public BaseResult DeleteBulkByOrderId(int orderId);
    }
}
