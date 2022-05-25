using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Repositories.Flowers;

namespace FlowersBEWebApi.Services.Flowers
{
    public class FlowersService : IFlowersService
    {
        private readonly IFlowerRepository _repository;
        private readonly ILogger<FlowersService> _logger;
        private readonly DataContext _context;

        public FlowersService(IFlowerRepository repository, ILogger<FlowersService> logger, DataContext dataContext)
        {
            _repository = repository;
            _logger = logger;
            _context = dataContext;
        }

        public BaseResult DeleteFlowerById(int id)
        {
            _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)} ({id})");
            try
            {
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)}: Flower with the following ID: ({id}) was not found");
                    return new BaseResult(false, 404, "Flower with the given ID not found");
                }
                _repository.Delete(id);
                _context.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(DeleteFlowerById)} ({id}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetAll()
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetAll)}");
                var flowers = _repository.GetAll();
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetAll)}: No flowers were found");
                    return new BaseResult(false, 404, "No flowers were found");
                }
                var flowersModelled = flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
                return new BaseResult(true, 200, "", flowersModelled);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetAll)}: ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetByCategory(string category)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetByCategory)} ({category})");
                var flowers = _repository.GetByCategory(category);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetByCategory)}: no flowers were found for category: ({category})");
                    return new BaseResult(false, 404, "No flowers found for the given category");
                }
                var catFlower = flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
                return new BaseResult(true, 200, "", catFlower);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetByCategory)} (Category: {category}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetById(int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetById)} (Id: {id})");
                var flower = _repository.Get(id);
                if (flower is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetById)}: no flowers were found with ID: ({id})");
                    return new BaseResult(false, 404, "Flower with the given ID not found");
                }
                var modelledFlower = new FlowerModel 
                { 
                    Id = id, 
                    Name = flower.Name, 
                    Category = flower.Category, 
                    Price = flower.Price, 
                    ImagePath = flower.ImagePath 
                };
                return new BaseResult(true, 200, "", modelledFlower);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetById)} (Id: {id}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult GetByPrice(decimal price)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(GetByPrice)} (Price: {price})");
                var flowers = _repository.GetByPrice(price);
                if (flowers.Count == 0 || flowers is null)
                {
                    _logger.LogWarning($"[{nameof(FlowersService)}] {nameof(GetByPrice)}: no flowers were found with price: ({price})");
                    return new BaseResult(false, 404, "No flowers were found with the given price");
                }
                var pricedFlowers = flowers.Select(x => new FlowerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                }).ToList();
                return new BaseResult(true, 200, "", pricedFlowers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(GetByPrice)} (Price: {price}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult SaveNewFlower(FlowerModel flower)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(SaveNewFlower)} ({flower.ToString()})");
                _repository.Add(new Flower
                {
                    Name = flower.Name,
                    Price = flower.Price,
                    Category = flower.Category,
                    ImagePath = flower.ImagePath,
                });
                _context.SaveChanges();
                return new BaseResult(true, 201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(SaveNewFlower)} ({flower.ToString()}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }

        public BaseResult UpdateFlowerData(FlowerModel flower, int id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(FlowersService)}] {nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()})");

                var checkRes = GetById(id);
                if (!checkRes.Success)
                    return checkRes;

                _repository.Update(new Flower
                {
                    Id = id,
                    Name = flower.Name,
                    Price = flower.Price,
                    Category = flower.Category,
                    ImagePath = flower.ImagePath,
                });
                _context.SaveChanges();
                return new BaseResult(true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{nameof(FlowersService)}] {nameof(UpdateFlowerData)} (Id: {id}, {flower.ToString()}): ({ex})");
                return new BaseResult(false, 500, "Internal server error");
            }
        }
    }
}
