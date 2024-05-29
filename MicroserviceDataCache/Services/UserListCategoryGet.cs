using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;

namespace MicroserviceDataCache.Services;

public interface IUserListCategoryGet
{
    List<UserCategory> GetUserCategories();
}

public class UserListCategoryGet : IUserListCategoryGet
{
    private readonly AppDbContext _context;

    public UserListCategoryGet(AppDbContext context)
    {
        _context = context;
    }

    public List<UserCategory> GetUserCategories()
    {
        return _context.UserCategories.ToList();
    }
}