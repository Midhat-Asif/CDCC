using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Backend.Data;
using Microsoft.AspNetCore.Cors;
using System;
using System.IO;
using System.Linq;

[Route("api/upload")]
[ApiController]
[EnableCors("AllowReactApp")]
public class UploadController : ControllerBase
{
    private readonly YourDbContext _dbContext; // Replace YourDbContext with your actual DbContext

    public UploadController(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            Console.WriteLine("Invalid file");
            return BadRequest("Invalid file");
        }

        try
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            using var package = new ExcelPackage(stream);

            // Find the "SalesOrders" sheet by name
            var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "SalesOrders");

            if (worksheet == null)
            {
                Console.WriteLine("Sheet 'SalesOrders' not found in the Excel file");
                return BadRequest("Sheet 'SalesOrders' not found in the Excel file");
            }

            // Create a new project entry
            var project = new Project { ProjectName = file.FileName };
            _dbContext.Projects.Add(project);

            _dbContext.SaveChanges(); // Save changes to the database to get the ProjectID

            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                var salesData = new myOrders
                {
                    ProjectID = project.ProjectID, // Associate the sales data with the project
                    OrderDate = DateTime.Parse(worksheet.Cells[row, 1].Text),
                    Region = worksheet.Cells[row, 2].Text,
                    Rep = worksheet.Cells[row, 3].Text,
                    Item = worksheet.Cells[row, 4].Text,
                    Units = int.Parse(worksheet.Cells[row, 5].Text),
                    UnitCost = decimal.Parse(worksheet.Cells[row, 6].Text),
                    Total = decimal.Parse(worksheet.Cells[row, 7].Text)
                };
                Console.WriteLine(salesData.Region);
                Console.WriteLine(salesData.OrderDate);
                Console.WriteLine(salesData.ProjectID);
                Console.WriteLine(salesData.SalesID);

                _dbContext.MyOrders.Add(salesData);
            }

            _dbContext.SaveChanges(); // Save changes to the database

            Console.WriteLine("File uploaded and data processed successfully");
            return Ok("File uploaded and data processed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
