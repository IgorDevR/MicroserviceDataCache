using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using MicroserviceDataCache.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDataCache.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminTasksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAdminTaskUserCacheAggregate _adminTaskUserCacheAggregate;

    public AdminTasksController(AppDbContext context, IAdminTaskUserCacheAggregate adminTaskUserCacheAggregate)
    {
        _context = context;
        _adminTaskUserCacheAggregate = adminTaskUserCacheAggregate;
    }

    [HttpGet("tasks")]
    public ActionResult<IEnumerable<AdminTask>> GetAdminTasks()
    {
        return _context.AdminTasks.ToList();
    }

    [HttpPost("tasks")]
    public IActionResult AddAdminTask(AdminTask task)
    {
        _context.AdminTasks.Add(task);
        _context.SaveChanges();
        return Ok(task);
    }

    [HttpGet("task-caches")]
    public ActionResult<IEnumerable<AdminTaskCache>> GetAdminTaskCache()
    {
        return _context.AdminTaskCaches.ToList();
    }

    [HttpPost("task-cache/aggregate")]
    public IActionResult AggregateAdminTasks()
    {
        _adminTaskUserCacheAggregate.AggregateAdminTasks();
        return Ok();
    }
}