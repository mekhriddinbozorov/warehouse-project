using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

// builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var connectionString = builder.Configuration.GetConnectionString("Mysql");
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
{
     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

app.MapControllers();

app.Run();
