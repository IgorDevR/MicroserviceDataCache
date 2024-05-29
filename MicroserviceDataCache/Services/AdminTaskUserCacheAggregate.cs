using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;

namespace MicroserviceDataCache.Services
{
    public interface IAdminTaskUserCacheAggregate
    {
        void AggregateAdminTasks();
    }

    public class AdminTaskUserCacheAggregate : IAdminTaskUserCacheAggregate

    {
        private readonly AppDbContext _context;

        public AdminTaskUserCacheAggregate(AppDbContext context)
        {
            _context = context;
        }

        public void AggregateAdminTasks()
        {
            var adminTaskCounts = _context.AdminTasks
                .GroupBy(t => t.AdminId)
                .Select(g => new { AdminId = g.Key, TaskCount = g.Count() })
                .ToList();

            foreach (var taskCount in adminTaskCounts)
            {
                var cacheEntry = _context.AdminTaskCaches
                    .SingleOrDefault(c => c.AdminId == taskCount.AdminId);

                if (cacheEntry != null)
                {
                    cacheEntry.TaskCount = taskCount.TaskCount;
                }
                else
                {
                    _context.AdminTaskCaches.Add(new AdminTaskCache
                    {
                        AdminId = taskCount.AdminId,
                        TaskCount = taskCount.TaskCount
                    });
                }
            }

            _context.SaveChanges();
        }
    }
}