using FlowersBEWebApi.Models;

namespace FlowersBEWebApi.Services.Flowers
{
    public interface IFlowersService
    {
        public bool SaveNewFlower(FlowerModel flower);
        public bool DeleteFlowerById(int id);
        public bool UpdateFlowerData(FlowerModel flower, int id);
        public List<FlowerModel> GetByPrice(decimal price);
        public List<FlowerModel> GetByCategory(string category);
        public List<FlowerModel> GetAll();
        public FlowerModel GetById(int id);
    }
}
