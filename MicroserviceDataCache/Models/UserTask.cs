using System.Text.Json.Serialization;

namespace MicroserviceDataCache.Models;

public class UserTask
{
    [JsonIgnore]
    public long Id { get; set; }
    public long TaskId { get; set; }
    public long UserId { get; set; }
}