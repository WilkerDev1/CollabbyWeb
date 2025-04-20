using Collabby.Application.Interfaces;
using Collabby.Application.Services.Interfaces;
using Collabby.Application.Services;
using Collabby.infrastructure.Context;
using Collabby.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// ...

// Registrar EF Context
builder.Services.AddDbContext<CollabbyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Registrar servicios de Application
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();