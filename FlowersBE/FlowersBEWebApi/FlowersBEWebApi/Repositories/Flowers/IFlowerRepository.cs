using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Flowers
{
    public interface IFlowerRepository
    {
        public List<Flower> GetAll();
        public Flower Get(int id);
        public List<Flower> GetByCategory (string category);
        public List<Flower> GetByPrice (decimal price);
        public void Add (Flower flower);
        public void Update (Flower flower);
        public void Delete (int id);
    }
}
