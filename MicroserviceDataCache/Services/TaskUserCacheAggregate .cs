using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;

namespace MicroserviceDataCache.Services;

public interface ITaskUserCacheAggregate
{
    void AggregateUserTasks();
}

public class TaskUserCacheAggregate : ITaskUserCacheAggregate
{
    private readonly AppDbContext _context;

    public TaskUserCacheAggregate(AppDbContext context)
    {
        _context = context;
    }

    public void AggregateUserTasks()
    {
        var taskCounts = _context.UserTasks
            .GroupBy(t => t.UserId)
            .Select(g => new { UserId = g.Key, TaskCount = g.Count() })
            .ToList();

        foreach (var taskCount in taskCounts)
        {
            var cacheEntry = _context.UserTaskCaches
                .SingleOrDefault(c => c.UserId == taskCount.UserId);

            if (cacheEntry != null)
            {
                cacheEntry.TaskCount = taskCount.TaskCount;
            }
            else
            {
                _context.UserTaskCaches.Add(new UserTaskCache
                {
                    UserId = taskCount.UserId,
                    TaskCount = taskCount.TaskCount
                });
            }
        }

        _context.SaveChanges();
    }
}