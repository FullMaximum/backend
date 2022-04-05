using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Repositories;

namespace FlowersBEWebApi.Services
{
    public class BasicService : IBasicService
    {
        private readonly IBasicRepository _basicRepository;

        public BasicService(IBasicRepository basicRepository)
        {
            _basicRepository = basicRepository;
        }

        public void Add(UserBase user) => _basicRepository.Add(user);

        public List<UserBase> GetUsers() => _basicRepository.GetAll();
    }
}
