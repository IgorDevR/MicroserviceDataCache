using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDataCache.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTasksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITaskUserCacheAggregate _taskUserCacheAggregate;

    public UserTasksController(AppDbContext context, ITaskUserCacheAggregate taskUserCacheAggregate)
    {
        _context = context;
        _taskUserCacheAggregate = taskUserCacheAggregate;
    }

    [HttpGet("tasks")]
    public ActionResult<IEnumerable<UserTask>> GetUserTasks()
    {
        return _context.UserTasks.ToList();
    }

    [HttpPost("tasks")]
    public IActionResult AddUserTask(UserTask task)
    {
        _context.UserTasks.Add(task);
        _context.SaveChanges();
        return Ok(task);
    }

    [HttpGet("task-caches")]
    public ActionResult<IEnumerable<UserTaskCache>> GetUserTaskCache()
    {
        return _context.UserTaskCaches.ToList();
    }

    [HttpPost("task-cache/aggregate")]
    public IActionResult AggregateUserTasks()
    {
        _taskUserCacheAggregate.AggregateUserTasks();
        return Ok();
    }
}