using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDataCacheTests;

[TestClass]
public class TaskUserCacheAggregateResponsibilityTests
{
    private AppDbContext _context;
    private ITaskUserCacheAggregateResponsibility _taskUserCacheAggregateResponsibility;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _taskUserCacheAggregateResponsibility = new TaskUserCacheAggregateResponsibility(_context);

        _context.TaskResponsibilities.AddRange(new List<TaskResponsibility>
        {
            new TaskResponsibility { UserId = 1, TaskId = 1, Responsibility = "Owner" },
            new TaskResponsibility { UserId = 1, TaskId = 2, Responsibility = "Contributor" },
            new TaskResponsibility { UserId = 2, TaskId = 3, Responsibility = "Owner" }
        });
        _context.SaveChanges();
    }

    [TestMethod]
    public void AggregateUserTaskResponsibilities_ShouldUpdateCache()
    {
        _taskUserCacheAggregateResponsibility.AggregateUserTaskResponsibilities();

        var cacheEntries = _context.UserTaskResponsibilityCaches.ToList();
        Assert.AreEqual(3, cacheEntries.Count);
        Assert.AreEqual("Owner", cacheEntries.Single(c => c.UserId == 1 && c.TaskId == 1).Responsibility);
        Assert.AreEqual("Contributor", cacheEntries.Single(c => c.UserId == 1 && c.TaskId == 2).Responsibility);
        Assert.AreEqual("Owner", cacheEntries.Single(c => c.UserId == 2 && c.TaskId == 3).Responsibility);
    }

    [TestCleanup]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}