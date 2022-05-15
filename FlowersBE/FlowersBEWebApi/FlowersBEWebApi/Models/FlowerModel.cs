namespace FlowersBEWebApi.Models
{
    public class FlowerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }

        public override string ToString()
        {
            return "{Id:" + Id + "},{Name:" + Name + "},{Price:" + Price + "},{Category:" + Category + "},{Path:" + ImagePath + "}";
        }
    }
}
