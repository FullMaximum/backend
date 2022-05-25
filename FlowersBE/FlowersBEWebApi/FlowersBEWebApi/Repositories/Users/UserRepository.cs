using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.AppUsers.Add(user);
        }

        public User Get(int id)
        {
            return _context.AppUsers.FirstOrDefault(x => x.Id == id);
        }

        public User GetByCredentials(string username, string password)
        {
            return _context.AppUsers.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }
}
