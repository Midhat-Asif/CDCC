using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "Server=localhost;Database=master;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;"; // Replace with your connection string name
builder.Services.AddDbContext<YourDbContext>(options =>
{
    options.UseSqlServer(connectionString); // Use SQL Server for Azure SQL Edge
});

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Replace with your React app's URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors("AllowReactApp");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        // Log the exception
        // You can use a logging library like Serilog or write to a log file here
        Console.WriteLine($"Error: {exception.Message}");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("An internal server error occurred. Please try again later.");
    });
});

app.Run();
