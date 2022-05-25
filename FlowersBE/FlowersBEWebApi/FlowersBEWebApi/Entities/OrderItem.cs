using System.ComponentModel.DataAnnotations;

namespace FlowersBEWebApi.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FlowerId { get; set; }
        public int Count { get; set; }
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
