using System.Text.Json.Serialization;

namespace MicroserviceDataCache.Models;

public class UserTaskCache
{               
    public long Id { get; set; }
    public long UserId { get; set; }
    public long TaskCount { get; set; }
}