using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleAPI.DataAccess;
using SimpleAPI.DataAccess.Model;
using SimpleAPI.DataAccess.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddTransient<UserService>();

var app = builder.Build();

var firstNames = new List<string>
{
    "Ksusha",
    "Vadim",
    "Bonya"
};

var lastName = new List<string>
{
    "Smyshlyaeva",
    "Byzov",
    "Boniface"
};

var ages = Enumerable.Range(11, 13).ToList();

app.MapGet("users", async (UserService userService) =>
{
    return await userService.GetAsync();
});

app.MapGet("users/{id}", async (UserService userService, int id) =>
{
    return await userService.GetAsync(id);
});

app.MapGet("users/create", async (UserService userService) =>
{
    var random = new Random();
    var user = new User();
    user.FirstName = firstNames[random.Next(3)];
    user.LastName = lastName[random.Next(3)];
    user.Age = ages[random.Next(ages.Count)];
    user.CreatedAt = DateTime.Now;
    return await userService.CreateAsync(user);
});

app.MapGet("users/delete", async (UserService userService) =>
{
    await userService.DeleteAsync();
});

app.UseHttpsRedirection();

app.Run();
