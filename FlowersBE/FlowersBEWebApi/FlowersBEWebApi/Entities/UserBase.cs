using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowersBEWebApi.Entities
{
    public class UserBase
    {
        [ConcurrencyCheck]
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
