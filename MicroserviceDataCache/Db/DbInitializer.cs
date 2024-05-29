using MicroserviceDataCache.Models;

namespace MicroserviceDataCache.Db;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        // Проверка наличия данных
        if (context.UserTasks.Any())
        {
            return; // База данных уже инициализирована
        }

        var userTasks = new[]
        {
            new UserTask { TaskId = 1, UserId = 1 },
            new UserTask { TaskId = 2, UserId = 1 },
            new UserTask { TaskId = 3, UserId = 2 }
        };
        context.UserTasks.AddRange(userTasks);

        var adminTasks = new[]
        {
            new AdminTask { TaskId = 1, AdminId = 1 },
            new AdminTask { TaskId = 2, AdminId = 1 },
            new AdminTask { TaskId = 3, AdminId = 2 }
        };
        context.AdminTasks.AddRange(adminTasks);

        var userCategories = new[]
        {
            new UserCategory { UserId = 1, CategoryId = 10 },
            new UserCategory { UserId = 2, CategoryId = 20 }
        };
        context.UserCategories.AddRange(userCategories);

        var taskResponsibilities = new[]
        {
            new TaskResponsibility { UserId = 1, TaskId = 1, Responsibility = "Owner" },
            new TaskResponsibility { UserId = 1, TaskId = 2, Responsibility = "Contributor" },
            new TaskResponsibility { UserId = 2, TaskId = 3, Responsibility = "Owner" }
        };
        context.TaskResponsibilities.AddRange(taskResponsibilities);

        context.SaveChanges();
    }
}