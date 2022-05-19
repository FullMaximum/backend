using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;

namespace FlowersBEWebApi.Services
{
    public interface IBasicService
    {
        public BaseResult GetUsers();
        public void Add(UserBase user);
        public void Update(UserBase user, int id);
    }
}
