using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories
{
    public class BasicRepository : IBasicRepository
    {
        private readonly DataContext _context;

        public BasicRepository(DataContext context)
        {
            _context = context;
        }
        public List<UserBase> GetAll() => _context.Users.ToList();
    }
}
