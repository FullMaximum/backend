using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Flowers;

namespace FlowersBEWebApi.Services.Flowers
{
    public class FlowersService : IFlowersService
    {
        private readonly IFlowerRepository _repository;
        private readonly ILogger<FlowersService> _logger;

        public FlowersService(IFlowerRepository repository, ILogger<FlowersService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public bool DeleteFlowerById(int id)
        {
            _logger.LogInformation($"{nameof(DeleteFlowerById)} ({id})");
            try
            {
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    _logger.LogWarning($"{nameof(DeleteFlowerById)}: Flower with the following ID: ({id}) was not found");
                    return false;
                }
                _repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteFlowerById)} ({id}): ({ex})");
                return false;
            }
        }

        public List<FlowerModel> GetAll()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAll)}");
                var flowers = _repository.GetAll();
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"{nameof(GetAll)}: no flowers were found");
                    return new List<FlowerModel>();
                }
                return flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetAll)}: ({ex})");
                return new List<FlowerModel>();
            }
        }

        public List<FlowerModel> GetByCategory(string category)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByCategory)} ({category})");
                var flowers = _repository.GetByCategory(category);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"{nameof(GetByCategory)}: no flowers were found for category: ({category})");
                    return new List<FlowerModel>();
                }
                return flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetByCategory)} (Category: {category}): ({ex})");
                return new List<FlowerModel>();
            }
        }

        public FlowerModel GetById(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetById)} (Id: {id})");
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    return null;
                }
                return new FlowerModel 
                { 
                    Id = id, 
                    Name = flower.Name, 
                    Category = flower.Category, 
                    Price = flower.Price, 
                    ImagePath = flower.ImagePath 
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetById)} (Id: {id}): ({ex})");
                return null;
            }
        }

        public List<FlowerModel> GetByPrice(decimal price)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByPrice)} (Price: {price})");
                var flowers = _repository.GetByPrice(price);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"{nameof(GetByPrice)}: no flowers were found with price: ({price})");
                    return new List<FlowerModel>();
                }
                return flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetByPrice)} (Price: {price}): ({ex})");
                return new List<FlowerModel>();
            }
        }

        public bool SaveNewFlower(FlowerModel flower)
        {
            try
            {
                _logger.LogInformation($"{nameof(SaveNewFlower)} ({flower.ToString()})");
                _repository.Add(new Flower
                {
                    Name = flower.Name,
                    Price = flower.Price,
                    Category = flower.Category,
                    ImagePath = flower.ImagePath,
                });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(SaveNewFlower)} ({flower.ToString()}): ({ex})");
                return false;
            }
        }

        public bool UpdateFlowerData(FlowerModel flower, int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()})");
                _repository.Update(new Flower
                {
                    Id = id,
                    Name = flower.Name,
                    Price = flower.Price,
                    Category = flower.Category,
                    ImagePath = flower.ImagePath,
                });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()}): ({ex})");
                return false;
            }
        }
    }
}
