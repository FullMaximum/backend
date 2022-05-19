using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories
{
    public interface IBasicRepository
    {
        public List<UserBase> GetAll();
        public void Add(UserBase user);
        public void Update(UserBase user);
    }
}
