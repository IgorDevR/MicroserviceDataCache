using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceDataCacheTests;

[TestClass]
public class UserListCategoryGetTests
{
    private AppDbContext _context;
    private IUserListCategoryGet _userListCategoryGet;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _userListCategoryGet = new UserListCategoryGet(_context);

        _context.UserCategories.AddRange(new List<UserCategory>
        {
            new UserCategory { UserId = 1, CategoryId = 10 },
            new UserCategory { UserId = 2, CategoryId = 20 }
        });
        _context.SaveChanges();
    }

    [TestMethod]
    public void GetUserCategories_ShouldReturnCategories()
    {
        var categories = _userListCategoryGet.GetUserCategories();

        Assert.IsNotNull(categories);
        Assert.AreEqual(2, categories.Count);
        Assert.AreEqual(10, categories.Single(c => c.UserId == 1).CategoryId);
        Assert.AreEqual(20, categories.Single(c => c.UserId == 2).CategoryId);
    }

    [TestCleanup]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}