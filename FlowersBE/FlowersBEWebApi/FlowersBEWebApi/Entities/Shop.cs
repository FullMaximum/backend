using FlowersBEWebApi.Enums;

namespace FlowersBEWebApi.Entities
{
    public class Shop
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public ShopStatus Status { get; set; }
    }
}
