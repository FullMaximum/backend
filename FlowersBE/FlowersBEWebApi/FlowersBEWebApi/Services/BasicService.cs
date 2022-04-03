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
        public List<UserBase> GetUsers()
        {
            return _basicRepository.GetAll();
        }
    }
}
