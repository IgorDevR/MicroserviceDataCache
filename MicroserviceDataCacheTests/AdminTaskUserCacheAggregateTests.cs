using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDataCacheTests;

[TestClass]
public class AdminTaskUserCacheAggregateTests
{
    private AppDbContext _context;
    private IAdminTaskUserCacheAggregate _adminTaskUserCacheAggregate;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _adminTaskUserCacheAggregate = new AdminTaskUserCacheAggregate(_context);

        _context.AdminTasks.AddRange(new List<AdminTask>
        {
            new AdminTask { TaskId = 1, AdminId = 1 },
            new AdminTask { TaskId = 2, AdminId = 1 },
            new AdminTask { TaskId = 3, AdminId = 2 }
        });
        _context.SaveChanges();
    }

    [TestMethod]
    public void AggregateAdminTasks_ShouldUpdateCache()
    {
        _adminTaskUserCacheAggregate.AggregateAdminTasks();

        var cacheEntries = _context.AdminTaskCaches.ToList();
        Assert.AreEqual(2, cacheEntries.Count);
        Assert.AreEqual(2, cacheEntries.Single(c => c.AdminId == 1).TaskCount);
        Assert.AreEqual(1, cacheEntries.Single(c => c.AdminId == 2).TaskCount);
    }

    [TestCleanup]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}