using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Flowers
{
    public interface IFlowersService
    {
        public BaseResult SaveNewFlower(FlowerModel flower);
        public BaseResult DeleteFlowerById(int id);
        public BaseResult UpdateFlowerData(FlowerModel flower, int id);
        public BaseResult GetByPrice(decimal price);
        public BaseResult GetByCategory(string category);
        public BaseResult GetAll();
        public BaseResult GetById(int id);
        public BaseResult GetByShopId(long shopId);
    }
}
