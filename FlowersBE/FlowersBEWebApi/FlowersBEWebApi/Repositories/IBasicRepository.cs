using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Repositories
{
    public interface IBasicRepository
    {
        public List<UserBase> GetAll();
    }
}
