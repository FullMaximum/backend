using FlowersBEWebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlowersBEWebApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public long ShopId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderTotal { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
