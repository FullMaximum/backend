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

        public void Add(UserBase user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<UserBase> GetAll() => _context.Users.ToList();


    }
}
