using ManagementSystem.BLL.Abstract;
using ManagementSystem.BLL.ValidationRules;
using Newtonsoft.Json;
using ManagementSystem.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScopeBLL();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<UserValidator>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
