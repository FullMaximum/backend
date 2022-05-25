namespace FlowersBEWebApi.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FlowerId { get; set; }
        public int Count { get; set; }
        public byte[]? RowVersion { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, OrderId: {OrderId}, FlowerId:{FlowerId}, Count: {Count}";
        }
    }
}
