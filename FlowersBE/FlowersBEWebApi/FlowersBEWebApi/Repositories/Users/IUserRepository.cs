using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories.Users
{
    public interface IUserRepository
    {
        public User Get(int id);
        public User GetByCredentials(string username, string password);
        public void Add (User user);
    }
}
