using FlowersBEWebApi.Enums;

namespace FlowersBEWebApi.Entities
{
    public class Shop
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? OpeningHour { get; set; }
        public string? ClosingHour { get; set; }
        public float Rating { get; set; }
        public ShopStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
