using FlowersBEWebApi.Enums;

namespace FlowersBEWebApi.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public long ShopId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderTotal { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[]? RowVersion { get; set; }
        public List<Item> Items { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, UserId: {UserId}, ShopId: {ShopId}, Status: {Status}, OrderTotal: {OrderTotal}, DeliveryAddress: {DeliveryAddress}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
        }
    }

    public class Item
    {
        public int FlowerId { get; set; }
        public int Count { get; set; }
    }
}
