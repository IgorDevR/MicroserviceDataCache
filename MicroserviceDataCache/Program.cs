using MicroserviceDataCache.Db;
using MicroserviceDataCache.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<ITaskUserCacheAggregate, TaskUserCacheAggregate>();
builder.Services.AddScoped<IAdminTaskUserCacheAggregate, AdminTaskUserCacheAggregate>();
builder.Services.AddScoped<IUserListCategoryGet, UserListCategoryGet>();
builder.Services.AddScoped<ITaskUserCacheAggregateResponsibility, TaskUserCacheAggregateResponsibility>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);

    var userTaskCacheAggregate = scope.ServiceProvider.GetRequiredService<ITaskUserCacheAggregate>();
    userTaskCacheAggregate.AggregateUserTasks();

    var adminTaskUserCacheAggregate = scope.ServiceProvider.GetRequiredService<IAdminTaskUserCacheAggregate>();
    adminTaskUserCacheAggregate.AggregateAdminTasks();   
    
    var aggregateUserTaskResponsibilities = scope.ServiceProvider.GetRequiredService<ITaskUserCacheAggregateResponsibility>();
    aggregateUserTaskResponsibilities.AggregateUserTaskResponsibilities();
}

app.Run();