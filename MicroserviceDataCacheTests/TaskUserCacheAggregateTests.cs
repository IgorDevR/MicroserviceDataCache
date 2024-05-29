using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDataCacheTests;

[TestClass]
public class TaskUserCacheAggregateTests
{
    private AppDbContext _context;
    private ITaskUserCacheAggregate _taskUserCacheAggregate;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _taskUserCacheAggregate = new TaskUserCacheAggregate(_context);

        _context.UserTasks.AddRange(new List<UserTask>
        {
            new UserTask { TaskId = 1, UserId = 1 },
            new UserTask { TaskId = 2, UserId = 1 },
            new UserTask { TaskId = 3, UserId = 2 }
        });
        _context.SaveChanges();
    }

    [TestMethod]
    public void AggregateUserTasks_ShouldUpdateCache()
    {
        _taskUserCacheAggregate.AggregateUserTasks();

        var cacheEntries = _context.UserTaskCaches.ToList();
        Assert.AreEqual(2, cacheEntries.Count);
        Assert.AreEqual(2, cacheEntries.Single(c => c.UserId == 1).TaskCount);
        Assert.AreEqual(1, cacheEntries.Single(c => c.UserId == 2).TaskCount);
    }

    [TestCleanup]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}