using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Services
{
    public interface IBasicService
    {
        public List<UserBase> GetUsers();
        public void Add(UserBase user);
    }
}
