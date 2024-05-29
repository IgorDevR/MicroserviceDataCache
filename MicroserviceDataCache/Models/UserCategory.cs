using System.Text.Json.Serialization;

namespace MicroserviceDataCache.Models;

public class UserCategory
{
    [JsonIgnore]
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CategoryId { get; set; }
}