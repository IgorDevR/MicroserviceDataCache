using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;

namespace MicroserviceDataCache.Services;

public interface ITaskUserCacheAggregateResponsibility
{
    void AggregateUserTaskResponsibilities();
}

public class TaskUserCacheAggregateResponsibility : ITaskUserCacheAggregateResponsibility
{
    private readonly AppDbContext _context;

    public TaskUserCacheAggregateResponsibility(AppDbContext context)
    {
        _context = context;
    }

    public void AggregateUserTaskResponsibilities()
    {
        var taskResponsibilities = _context.TaskResponsibilities
            .Select(tr => new { tr.UserId, tr.TaskId, tr.Responsibility })
            .ToList();

        foreach (var responsibility in taskResponsibilities)
        {
            var cacheEntry = _context.UserTaskResponsibilityCaches
                .SingleOrDefault(c => c.UserId == responsibility.UserId && c.TaskId == responsibility.TaskId);

            if (cacheEntry != null)
            {
                cacheEntry.Responsibility = responsibility.Responsibility;
            }
            else
            {
                _context.UserTaskResponsibilityCaches.Add(new UserTaskResponsibilityCache
                {
                    UserId = responsibility.UserId,
                    TaskId = responsibility.TaskId,
                    Responsibility = responsibility.Responsibility
                });
            }
        }

        _context.SaveChanges();
    }
}