using System.Text.Json.Serialization;

namespace MicroserviceDataCache.Models;

public class TaskResponsibility
{
    [JsonIgnore]
    public long Id { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public string Responsibility { get; set; }
}