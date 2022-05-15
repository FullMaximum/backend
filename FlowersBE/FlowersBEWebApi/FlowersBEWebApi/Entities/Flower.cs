namespace FlowersBEWebApi.Entities
{
    public class Flower
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }
    }
}
