using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var app = builder.Build();

app.Run();
