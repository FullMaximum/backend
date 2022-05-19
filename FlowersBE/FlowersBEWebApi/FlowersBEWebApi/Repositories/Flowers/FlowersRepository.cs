using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Flowers
{
    public class FlowersRepository : IFlowerRepository
    {
        private readonly DataContext _context;

        public FlowersRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Flower flower)
        {
            _context.Flowers.Add(flower);
        }

        public Flower Get(int id)
        {
            return _context.Flowers.FirstOrDefault(x => x.Id == id);
        }

        public List<Flower> GetAll()
        {
            return _context.Flowers.ToList();
        }

        public List<Flower> GetByCategory(string category)
        {
            return _context.Flowers.Where(x => x.Category == category).ToList();
        }

        public List<Flower> GetByPrice(decimal price)
        {
            return _context.Flowers.Where(x => x.Price == price).ToList();
        }

        public void Update(Flower flower)
        {
            _context.Flowers.Update(flower);
        }

        public void Delete(int id)
        {
            _context.Flowers.Remove(Get(id));
        }
    }
}
