using MicroserviceDataCache.Db;
using MicroserviceDataCache.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDataCache.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserCategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("categories")]
    public ActionResult<IEnumerable<UserCategory>> GetUserCategories()
    {
        return _context.UserCategories.ToList();
    }

    [HttpPost("categories")]
    public IActionResult AddUserCategory(UserCategory category)
    {
        _context.UserCategories.Add(category);
        _context.SaveChanges();
        return Ok(category);
    }

    [HttpGet("task-responsibilities")]
    public ActionResult<IEnumerable<TaskResponsibility>> GetTaskResponsibilities()
    {
        return _context.TaskResponsibilities.ToList();
    }

    [HttpPost("task-responsibilities")]
    public IActionResult AddTaskResponsibility(TaskResponsibility responsibility)
    {
        _context.TaskResponsibilities.Add(responsibility);
        _context.SaveChanges();
        return Ok(responsibility);
    }
}