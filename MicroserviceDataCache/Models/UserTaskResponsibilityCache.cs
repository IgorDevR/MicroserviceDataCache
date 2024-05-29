namespace MicroserviceDataCache.Models;

public class UserTaskResponsibilityCache
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public string Responsibility { get; set; }
}