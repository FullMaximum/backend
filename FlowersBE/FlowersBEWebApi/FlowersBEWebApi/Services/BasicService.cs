using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Repositories;
using System.Diagnostics;

namespace FlowersBEWebApi.Services
{
    /// <summary>
    /// This is a "dummy" service, where we can try out new global functionality without fearing to break business logic
    /// </summary>
    public class BasicService : IBasicService
    {
        private readonly IBasicRepository _basicRepository;
        private readonly DataContext _context;
        private readonly ILogger<BasicService> _logger;

        public BasicService(IBasicRepository basicRepository, DataContext dataContext, ILogger<BasicService> logger)
        {
            _basicRepository = basicRepository;
            _context = dataContext;
            _logger = logger;
        }

        public void Add(UserBase user)
        {
            _basicRepository.Add(user);
            _context.SaveChanges();
        }
        

        public BaseResult GetUsers()
        {
            try
            {
                var users = _basicRepository.GetAll();
                return new BaseResult(true, 200, "", users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(BasicService)}] {nameof(GetUsers)}: ({ex})");
                return new BaseResult(false, 400, $"{ex}");
            }
        }
        

        public void Update(UserBase user, int id)
        {
            try
            {
                user.Id = id;
                _basicRepository.Update(user);
                Thread.Sleep(2500);
                _context.SaveChanges();
                Debug.WriteLine($"[BasicService] Update ({user}): updated successfuly");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[BasicService] Update ({user}): {ex}");
            }
        }
    }
}
