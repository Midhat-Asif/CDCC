using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/sales")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly YourDbContext _dbContext;

    public SalesController(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetSalesByProjectID(int projectID)
    {
        var filteredData = _dbContext.MyOrders
            .Where(o => o.ProjectID == projectID)
            .Select(o => new
            {
                o.SalesID,
                o.OrderDate,
                o.Region,
                o.Rep,
                o.Item,
                o.Units,
                o.UnitCost,
                o.Total
            })
            .ToList();

        return Ok(filteredData);
    }
}
