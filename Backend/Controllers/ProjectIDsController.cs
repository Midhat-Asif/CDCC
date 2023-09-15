using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Route("api/projectids")]
[ApiController]
public class ProjectIDsController : ControllerBase
{
    private readonly YourDbContext _dbContext;

    public ProjectIDsController(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetUniqueProjectIDs()
    {
        var projectIDs = _dbContext.MyOrders.Select(o => o.ProjectID).Distinct().ToList();
        return Ok(projectIDs);
    }
}
