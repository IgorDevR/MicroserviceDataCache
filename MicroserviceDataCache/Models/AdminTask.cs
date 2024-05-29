using System.Text.Json.Serialization;

namespace MicroserviceDataCache.Models;

public class AdminTask
{
    [JsonIgnore]
    public long Id { get; set; }
    public long TaskId { get; set; }
    public long AdminId { get; set; }
}