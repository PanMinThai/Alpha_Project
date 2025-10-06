using Alpha_Project.Modules.Tasks.Dto;
using Alpha_Project.Modules.Tasks.Query;
using Alpha_Project.Modules.Tasks.QueryHandler;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Domain.Mediator;
using Shared.Base.Infra.Common.Base;
using Shared.Base.Infra.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// ============= Dependency Injection =============
builder.Services.Scan(scan => scan
    .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);

// DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TaskPrimaryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TaskReadonlyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PrimaryDbContext, TaskPrimaryDbContext>();
builder.Services.AddScoped<ReadonlyDbContext, TaskReadonlyDbContext>();
// Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository2>();
// builder.Services.AddScoped<IUserRepository, UserRepository>()

builder.Services.AddScoped<IDispatcher, Dispatcher>();

// Service 
builder.Services.AddScoped<ILogService, ConsoleLogService>();
builder.Services.AddScoped<IEmailService, ConsoleEmailService>();

// Controllers
builder.Services.AddControllers();

// ============= Swagger =============
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============= Middleware pipeline =============
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
