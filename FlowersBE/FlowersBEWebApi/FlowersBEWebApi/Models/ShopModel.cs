namespace FlowersBEWebApi.Models
{
	public class ShopModel
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
        public decimal DeliveryPrice { get; set; }
        public float DeliveryDistance { get; set; }
        public float Rating { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Phone}, {ImagePath}, {Description}, {Address}, {City}, {OpeningHour}, {ClosingHour}, {Rating}";
        }

    }
}

